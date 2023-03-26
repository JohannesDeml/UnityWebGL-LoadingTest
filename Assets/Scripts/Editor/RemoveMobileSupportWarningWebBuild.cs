// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveMobileSupportWarningWebBuild.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

#if !UNITY_2020_1_OR_NEWER //Not needed anymore in 2020 and above
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// Removes a warning popup for mobile builds, that this platform might not be supported:
	/// "Please note that Unity WebGL is not currently supported on mobiles. Press OK if you wish to continue anyway."
	/// This warning only shows up for unity versions prior to 2020.1
	/// </summary>
	public class RemoveMobileSupportWarningWebBuild
	{
		[PostProcessBuild]
		public static void OnPostProcessBuild(BuildTarget target, string targetPath)
		{
			if (target != BuildTarget.WebGL)
			{
				return;
			}

			var buildFolderPath = Path.Combine(targetPath, "Build");
			var info = new DirectoryInfo(buildFolderPath);
			var files = info.GetFiles("*.js");
			for (int i = 0; i < files.Length; i++)
			{
				var file = files[i];
				var filePath = file.FullName;
				var text = File.ReadAllText(filePath);
				text = text.Replace("UnityLoader.SystemInfo.mobile", "false");

				Debug.Log("Removing mobile warning from " + filePath);
				File.WriteAllText(filePath, text);
			}
		}
	}
}
#endif