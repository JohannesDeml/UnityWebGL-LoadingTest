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
	/// Unity menu items for building the project for WebGL with the build script
	/// Helpful for testing the CI behavior
	/// </summary>
	public class BuildScriptMenu
	{
		private static readonly List<string> baseParameters = new List<string>()
		{
			"-projectPath", "",
			"-buildVersion", PlayerSettings.bundleVersion,
			"-androidVersionCode", PlayerSettings.Android.bundleVersionCode.ToString(CultureInfo.InvariantCulture)
		};

		[MenuItem("Tools/Build WebGL/webgl1")]
		public static void BuildWebGLDefault()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-webgl1";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/minsize")]
		public static void BuildWebGLMinSize()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-minsize-webgl1";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/debug")]
		public static void BuildWebGLDebug()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-debug";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
			BuildWithParameters(parameters);
		}

		[MenuItem("Tools/Build WebGL/webgl2")]
		public static void BuildWebGLWebGL2()
		{
			var parameters = new List<string>(baseParameters);
			string tag = $"{Application.unityVersion}-webgl2";
			SetBuildTarget(BuildTarget.WebGL, ref parameters);
			SetParameterValue("-autorunplayer", "true", ref parameters);
			SetParameterValue("-tag", tag, ref parameters);
			SetParameterValue("-customBuildPath", $"Builds/WebGL/{tag}", ref parameters);
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