// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityPackageScripts.cs">
//   Copyright (c) 2023 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace UnityBuilderAction
{
	/// <summary>
	/// Editor script to automatically update all packages in the project to the newest version
	/// Can be used in continuous integration (CI) or through menu items in the editor
	/// Uses non-blocking tasks, so you can continue working until the packages recompile
	/// </summary>
	public static class UnityPackageScripts
	{
		private static int ProgressId;
		private static ListRequest ListRequest;
		#if UNITY_2021_2_OR_NEWER
		private static AddAndRemoveRequest AddAndRemoveRequest;
		#else
		private static AddRequest AddRequest;
		private static Queue<string> PackagesToAdd;
		#endif

		private static bool IncludePrereleases;

		[MenuItem("Tools/Packages/Update to verified version")]
		public static void UpgradeAllPackagesToVerifiedVersion()
		{
			IncludePrereleases = false;
			StartPackageListUpdate();
		}

		[MenuItem("Tools/Packages/Update to latest pre-release")]
		public static void UpgradeAllPackagesToLatestCompatibleVersion()
		{
			IncludePrereleases = true;
			StartPackageListUpdate();
		}

		private static void StartPackageListUpdate()
		{
#if UNITY_2020_1_OR_NEWER
			ProgressId = Progress.Start("Update Packages",
			$"Update all packages to latest version (Include Pre-Releases: {IncludePrereleases})");
#endif
			ListRequest = Client.List();
			EditorApplication.update += OnWaitForPackageList;
		}

		private static void OnWaitForPackageList()
		{
			if (!ListRequest.IsCompleted)
			{
#if UNITY_2020_1_OR_NEWER
				Progress.Report(ProgressId, 0.1f, "Update package list");
#endif
				return;
			}

			EditorApplication.update -= OnWaitForPackageList;

			switch (ListRequest.Status)
			{
				case StatusCode.Success:
					Console.WriteLine("Package list updated, checking for newer package versions");
					UpdatePackages();
					break;
				case StatusCode.Failure:
					Console.WriteLine($"Retrieving package list failed! {ListRequest.Error}");
					EndUpdate(101);
					break;
				case StatusCode.InProgress:
					Console.WriteLine("Retrieving package list is still in progress!");
					EndUpdate(102);
					break;
				default:
					Console.WriteLine($"Unsupported status {ListRequest.Status}!");
					EndUpdate(103);
					break;
			}
		}

		private static void UpdatePackages()
		{
			List<string> toAdd = new List<string>();
			List<string> toRemove = new List<string>();
			foreach (var package in ListRequest.Result)
			{
				if (package.source == PackageSource.Embedded ||
					package.source == PackageSource.BuiltIn ||
					package.source == PackageSource.Local)
				{
					continue;
				}


				string latestVersion = IncludePrereleases ?
					package.versions.latestCompatible :
#if UNITY_2022_3_OR_NEWER
					package.versions.recommended;
#elif UNITY_2019_3_OR_NEWER
					package.versions.verified;
#else
					package.versions.recommended;
#endif
				if (package.version == latestVersion || string.IsNullOrEmpty(latestVersion))
				{
					continue;
				}

				Debug.Log($"Update {package.name} from {package.version} to {latestVersion}");
				toAdd.Add($"{package.name}@{latestVersion}");
			}

			if (toRemove.Count == 0 && toAdd.Count == 0)
			{
				Console.WriteLine("All packages up to date");
				EndUpdate(0);
				return;
			}

#if UNITY_2020_1_OR_NEWER
			Progress.Report(ProgressId, 0.5f, $"Updating {toAdd.Count} packages: {string.Join(", ", toAdd)}");
#endif

#if UNITY_2021_2_OR_NEWER
			AddAndRemoveRequest = Client.AddAndRemove(toAdd.ToArray(), toRemove.ToArray());
			EditorApplication.update += OnWaitForPackageUpdates;
#else
			PackagesToAdd = new Queue<string>(toAdd);
			AddRequest = Client.Add(PackagesToAdd.Dequeue());
			EditorApplication.update += OnWaitForSinglePackageUpdate;
#endif
		}

#if UNITY_2021_2_OR_NEWER
		private static void OnWaitForPackageUpdates()
		{
#if UNITY_2020_1_OR_NEWER
			Progress.Report(ProgressId, 0.5f, null);
#endif
			if (!AddAndRemoveRequest.IsCompleted)
			{
				return;
			}

			EditorApplication.update -= OnWaitForPackageUpdates;

			switch (AddAndRemoveRequest.Status)
			{
				case StatusCode.Success:
					Console.WriteLine($"Packages updated successful: {AddAndRemoveRequest.Result}");
					EndUpdate(0);
					break;
				case StatusCode.Failure:
					Console.WriteLine($"Updating package list failed! {AddAndRemoveRequest.Error}");
					EndUpdate(201);
					break;
				case StatusCode.InProgress:
					Console.WriteLine("Retrieving package list is still in progress!");
					EndUpdate(202);
					break;
				default:
					Console.WriteLine($"Unsupported status {AddAndRemoveRequest.Status}!");
					EndUpdate(203);
					break;
			}
		}
#else

		private static void OnWaitForSinglePackageUpdate()
		{
#if UNITY_2020_1_OR_NEWER
			Progress.Report(ProgressId, 0.5f, null);
#endif
			if (!AddRequest.IsCompleted)
			{
				return;
			}

			if (AddRequest.Status == StatusCode.Success)
			{
				if (PackagesToAdd.Count > 0)
				{
					// Update the next package
					AddRequest = Client.Add(PackagesToAdd.Dequeue());
					return;
				}
			}
			EditorApplication.update -= OnWaitForSinglePackageUpdate;

			switch (AddRequest.Status)
			{
				case StatusCode.Success:
					Console.WriteLine($"Packages updated successful: {AddRequest.Result}");
					EndUpdate(0);
					break;
				case StatusCode.Failure:
					Console.WriteLine($"Updating package list failed! {AddRequest.Error}");
					EndUpdate(201);
					break;
				case StatusCode.InProgress:
					Console.WriteLine("Retrieving package list is still in progress!");
					EndUpdate(202);
					break;
				default:
					Console.WriteLine($"Unsupported status {AddRequest.Status}!");
					EndUpdate(203);
					break;
			}
		}
#endif

		private static void EndUpdate(int returnValue)
		{
#if UNITY_2020_1_OR_NEWER
			Progress.Finish(ProgressId, returnValue == 0 ? Progress.Status.Succeeded : Progress.Status.Failed);
#endif
			Debug.Log($"Updating unity packages finished with exit code {returnValue}");

			if (Application.isBatchMode)
			{
				// Hack for unity-builder v3, since it is expecting a build result output
				Console.WriteLine(
					$"{Environment.NewLine}" +
					$"###########################{Environment.NewLine}" +
					$"#      Build results      #{Environment.NewLine}" +
					$"###########################{Environment.NewLine}" +
					$"{Environment.NewLine}" +
					$"Duration: 0{Environment.NewLine}" +
					$"Warnings: 0{Environment.NewLine}" +
					$"Errors: 0{Environment.NewLine}" +
					$"Size: 0 bytes{Environment.NewLine}" +
					$"{Environment.NewLine}"
				);

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
	}
}