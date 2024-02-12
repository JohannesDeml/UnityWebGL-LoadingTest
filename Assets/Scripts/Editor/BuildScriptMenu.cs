// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildScriptMenu.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace UnityBuilderAction
{
	/// <summary>
	/// Menu items for <see cref="BuildScript"> to build the project in the editor
	/// Helpful for testing the CI behavior and semi-automated builds
	/// </summary>
	public class BuildScriptMenu
	{
		private static readonly List<string> baseParameters = new List<string>()
		{
			"-projectPath", "",
			"-buildVersion", PlayerSettings.bundleVersion,
			"-androidVersionCode", PlayerSettings.Android.bundleVersionCode.ToString(CultureInfo.InvariantCulture),
		};

		// Unity 2023.1+ does not support webgl1 anymore
		#if !UNITY_2023_1_OR_NEWER
		[MenuItem("Tools/Build WebGL/webgl1")]
		public static void BuildWebGL1()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-webgl1-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/minsize-webgl1")]
		public static void BuildWebGL1MinSize()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-minsize-webgl1-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}
		#endif

		[MenuItem("Tools/Build WebGL/webgl2")]
		public static void BuildWebGL2()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-webgl2-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/minsize-webgl2")]
		public static void BuildWebGL2MinSize()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-minsize-webgl2-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}

#if UNITY_2023_2_OR_NEWER
		[MenuItem("Tools/Build WebGL/webgpu")]
		public static void BuildWebGpu()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-webgpu-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/minsize-webgpu")]
		public static void BuildWebGpuMinSize()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-minsize-webgpu-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}
#endif

		[MenuItem("Tools/Build WebGL/debug")]
		public static void BuildWebGLDebug()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-debug-manualBuild";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			SetParameterValue("-customBuildName", tag, ref parameters);
			BuildWithParameters(parameters);
		}

		private static void BuildWithParameters(List<string> parameters)
		{
			Debug.Log($"Build project with parameters [{string.Join(" ", parameters)}]");
			BuildScript.Build(parameters.ToArray());
		}

		private static void SetBuildTarget(BuildTarget target, ref List<string> parameters)
		{
			SetParameterValue("-buildTarget", target.ToString(), ref parameters);
		}

		private static void SetParameterValue(string parameter, string value, ref List<string> parameters)
		{
			parameters.Add(parameter);
			parameters.Add(value);
		}
	}
}