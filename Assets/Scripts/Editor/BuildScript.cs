// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildScript.cs">
//   Copyright (c) 2023 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityBuilderAction
{
	/// <summary>
	/// Used for building the project through continuous integration (CI) or semi-automated through menu items
	/// Supports logging in the editor and highly configurable WebGL
	/// Modified version of <see href="https://github.com/game-ci/documentation/blob/main/example/BuildScript.cs" />
	/// Tailored to the needs for <see href="https://github.com/JohannesDeml/UnityWebGL-LoadingTest" />
	/// </summary>
	public static class BuildScript
	{
		private static readonly string Eol = Environment.NewLine;
		private static bool LogVerboseBatchMode = true;
		private static bool LogVerboseInEditor = false;

		private static readonly string[] Secrets =
			{ "androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass" };

		private static BuildPlayerOptions buildPlayerOptions;

		[UsedImplicitly]
		public static void BuildWithCommandlineArgs()
		{
			string[] args = Environment.GetCommandLineArgs();
			Build(args);
		}

		public static void Build(string[] args)
		{
			buildPlayerOptions = new BuildPlayerOptions();

			// Gather values from args
			Dictionary<string, string> options = GetValidatedOptions(args);

			// Set version for this build
			PlayerSettings.bundleVersion = options["buildVersion"];
			PlayerSettings.macOS.buildNumber = options["buildVersion"];
			PlayerSettings.Android.bundleVersionCode = int.Parse(options["androidVersionCode"]);

			// Apply build target
			var buildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), options["buildTarget"]);
			switch (buildTarget)
			{
				case BuildTarget.Android:
				{
					EditorUserBuildSettings.buildAppBundle = options["customBuildPath"].EndsWith(".aab");
					if (options.TryGetValue("androidKeystoreName", out string keystoreName) &&
						!string.IsNullOrEmpty(keystoreName))
						PlayerSettings.Android.keystoreName = keystoreName;
					if (options.TryGetValue("androidKeystorePass", out string keystorePass) &&
						!string.IsNullOrEmpty(keystorePass))
						PlayerSettings.Android.keystorePass = keystorePass;
					if (options.TryGetValue("androidKeyaliasName", out string keyaliasName) &&
						!string.IsNullOrEmpty(keyaliasName))
						PlayerSettings.Android.keyaliasName = keyaliasName;
					if (options.TryGetValue("androidKeyaliasPass", out string keyaliasPass) &&
						!string.IsNullOrEmpty(keyaliasPass))
						PlayerSettings.Android.keyaliasPass = keyaliasPass;
					break;
				}
				case BuildTarget.StandaloneOSX:
					PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
					break;
				case BuildTarget.WebGL:
#if UNITY_2021_2_OR_NEWER
					// Use ASTC texture compression, since we are also targeting mobile versions - Don't use this for desktop only targets
					buildPlayerOptions.subtarget = (int)WebGLTextureSubtarget.ASTC;
#endif

					if (options.TryGetValue("tag", out string tagVersion) &&
						!string.IsNullOrEmpty(tagVersion))
					{
						string[] tagParameters = tagVersion.Split('-');
						if (tagParameters.Contains("minsize"))
						{
							PlayerSettings.WebGL.template = "PROJECT:Release";
							SetWebGlOptimization("size");
							PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;
							PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Master);
						}
						else if (tagParameters.Contains("debug"))
						{
							PlayerSettings.WebGL.template = "PROJECT:Develop";
							SetWebGlOptimization("size");
							PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.FullWithStacktrace;
							PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Debug);
#if UNITY_2021_2_OR_NEWER
							PlayerSettings.WebGL.debugSymbolMode = WebGLDebugSymbolMode.Embedded;
#else
							PlayerSettings.WebGL.debugSymbols = true;
#endif

#if UNITY_2022_2_OR_NEWER
							PlayerSettings.WebGL.showDiagnostics = true;
#endif
							buildPlayerOptions.options |= BuildOptions.Development;
						}
						else
						{
							PlayerSettings.WebGL.template = "PROJECT:Develop";
							// By default use the speed setting
							SetWebGlOptimization("speed");
						}

						if (tagParameters.Contains("webgl2") && !tagParameters.Contains("webgl1"))
						{
							PlayerSettings.SetGraphicsAPIs(BuildTarget.WebGL, new[] { GraphicsDeviceType.OpenGLES3 });
						}

						if (tagParameters.Contains("webgl1") && !tagParameters.Contains("webgl2"))
						{
							PlayerSettings.SetGraphicsAPIs(BuildTarget.WebGL, new[] { GraphicsDeviceType.OpenGLES2 });
						}

						if (tagParameters.Contains("webgl1") && tagParameters.Contains("webgl2"))
						{
							PlayerSettings.SetGraphicsAPIs(BuildTarget.WebGL, new[] { GraphicsDeviceType.OpenGLES2, GraphicsDeviceType.OpenGLES3 });
						}
					}

					break;
			}

			// Additional options for local builds
			if (!Application.isBatchMode)
			{
				if (options.TryGetValue("autorunplayer", out string autorunplayer))
				{
					buildPlayerOptions.options |= BuildOptions.AutoRunPlayer;
				}

				var projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length);
				BackupLastBuild($"{projectPath}/{options["customBuildPath"]}");
			}

			// Custom build
			Build(buildTarget, options["customBuildPath"]);
		}

		private static void BackupLastBuild(string buildPath)
		{
			if (Directory.Exists(buildPath))
			{
				string backupFolderPath = $"{buildPath}-Previous";
				if (Directory.Exists(backupFolderPath))
				{
					Directory.Delete(backupFolderPath, true);
				}
				Log($"Moving current build folder to backup location: {backupFolderPath}");
				Directory.Move(buildPath, backupFolderPath);
			}
			else if (File.Exists(buildPath))
			{
				string extension = Path.GetExtension(buildPath);
				string pathWithoutExtension = buildPath.Substring(0, buildPath.Length - extension.Length);
				string backupFilePath = $"{pathWithoutExtension}-Previous{extension}";
				if (File.Exists(backupFilePath))
				{
					File.Delete(backupFilePath);
				}
				Log($"Moving current build file to backup location: {backupFilePath}");
				File.Move(buildPath, backupFilePath);
			}
		}

		private static void SetWebGlOptimization(string value)
		{
#if UNITY_2019_4_OR_NEWER
			EditorUserBuildSettings.SetPlatformSettings(BuildPipeline.GetBuildTargetName(BuildTarget.WebGL),
				"CodeOptimization", value);
#else
			Log($"Setting {nameof(SetWebGlOptimization)} not supported by this unity version");
#endif
		}

		private static Dictionary<string, string> GetValidatedOptions(string[] args)
		{
			ParseCommandLineArguments(args, out Dictionary<string, string> validatedOptions);

			if (!validatedOptions.TryGetValue("projectPath", out string _))
			{
				LogError("Missing argument -projectPath");
				EndBuild(110);
			}

			if (!validatedOptions.TryGetValue("buildTarget", out string buildTarget))
			{
				LogError("Missing argument -buildTarget");
				EndBuild(120);
			}

			if (!Enum.IsDefined(typeof(BuildTarget), buildTarget ?? string.Empty))
			{
				EndBuild(121);
			}

			if (!validatedOptions.TryGetValue("customBuildPath", out string _))
			{
				LogError("Missing argument -customBuildPath");
				EndBuild(130);
			}

			const string defaultCustomBuildName = "TestBuild";
			if (!validatedOptions.TryGetValue("customBuildName", out string customBuildName))
			{
				LogError($"Missing argument -customBuildName, defaulting to {defaultCustomBuildName}.");
				validatedOptions.Add("customBuildName", defaultCustomBuildName);
			}
			else if (customBuildName == "")
			{
				LogError($"Invalid argument -customBuildName, defaulting to {defaultCustomBuildName}.");
				validatedOptions.Add("customBuildName", defaultCustomBuildName);
			}

			return validatedOptions;
		}

		private static void ParseCommandLineArguments(string[] args, out Dictionary<string, string> providedArguments)
		{
			providedArguments = new Dictionary<string, string>();

			LogVerbose(
				$"{Eol}" +
				$"###########################{Eol}" +
				$"#	Parsing settings	 #{Eol}" +
				$"###########################{Eol}" +
				$"{Eol}"
			);

			// Extract flags with optional values
			for (int current = 0, next = 1; current < args.Length; current++, next++)
			{
				// Parse flag
				bool isFlag = args[current].StartsWith("-");
				if (!isFlag) continue;
				string flag = args[current].TrimStart('-');

				// Parse optional value
				bool flagHasValue = next < args.Length && !args[next].StartsWith("-");
				string value = flagHasValue ? args[next].TrimStart('-') : "";
				bool secret = Secrets.Contains(flag);
				string displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

				// Assign
				LogVerbose($"Found flag \"{flag}\" with value {displayValue}.");
				providedArguments.Add(flag, value);
			}
		}

		private static void Build(BuildTarget buildTarget, string filePath)
		{
			string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();
			buildPlayerOptions.scenes = scenes;
			buildPlayerOptions.target = buildTarget;
			buildPlayerOptions.locationPathName = filePath;

			BuildSummary buildSummary = BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
			ReportSummary(buildSummary);
			ExitWithResult(buildSummary.result);
		}

		private static void ReportSummary(BuildSummary summary)
		{
			string summaryText = $"{Eol}" +
				$"###########################{Eol}" +
				$"#	  Build results	  #{Eol}" +
				$"###########################{Eol}" +
				$"{Eol}" +
				$"Duration: {summary.totalTime.ToString()}{Eol}" +
				$"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
				$"Errors: {summary.totalErrors.ToString()}{Eol}" +
				$"Size: {summary.totalSize.ToString()} bytes{Eol}" +
				$"{Eol}";

			if (summary.totalErrors == 0)
			{
				Log(summaryText);
			}
			else
			{
				LogError(summaryText);
			}
		}

		private static void ExitWithResult(BuildResult result)
		{
			switch (result)
			{
				case BuildResult.Succeeded:
					Log("Build succeeded!");
					EndBuild(0);
					break;
				case BuildResult.Failed:
					LogError("Build failed!");
					EndBuild(101);
					break;
				case BuildResult.Cancelled:
					LogError("Build cancelled!");
					EndBuild(102);
					break;
				case BuildResult.Unknown:
				default:
					LogError("Build result is unknown!");
					EndBuild(103);
					break;
			}
		}

		private static void EndBuild(int returnValue)
		{
			if (Application.isBatchMode)
			{
				EditorApplication.Exit(returnValue);
			}
			else
			{
				if (returnValue != 0)
				{
					throw new Exception($"BuildScript ended with non-zero exitCode: {returnValue}");
				}
			}
		}

		private static void LogVerbose(string message)
		{
			if(Application.isBatchMode)
			{
				if (LogVerboseBatchMode)
				{
					Console.WriteLine(message);
				}
			}
			else
			{
				if (LogVerboseInEditor)
				{
					Debug.Log(message);
				}
			}
		}

		private static void Log(string message)
		{
			if(Application.isBatchMode)
			{
				Console.WriteLine(message);
			}
			else
			{
				Debug.Log(message);
			}
		}

		private static void LogError(string message)
		{
			if(Application.isBatchMode)
			{
				Console.WriteLine(message);
			}
			else
			{
				Debug.LogError(message);
			}
		}
	}
}