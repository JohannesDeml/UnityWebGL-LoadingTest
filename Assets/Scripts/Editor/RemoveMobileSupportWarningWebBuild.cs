// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveMobileSupportWarningWebBuild.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Supyrb
{
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

				Debug.Log("Removing iOS warning from " + filePath);
				File.WriteAllText(filePath, text);
			}
		}
	}
}