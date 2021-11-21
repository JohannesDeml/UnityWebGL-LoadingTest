// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadingTime.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// Log predefined information to the console
	/// Runs with early execution order so the startup time is not influenced
	/// by execution times of other scripts in the scene.
	/// </summary>
	[DefaultExecutionOrder(-100)]
	public class WebGlLogger : MonoBehaviour
	{
		[SerializeField]
		private bool logStartTime = true;

		[SerializeField]
		private bool logMemory = true;

		private void Awake()
		{
			if (logStartTime)
			{
				WebGlPlugins.LogStartTime();
			}

			if (logMemory)
			{
				WebGlPlugins.LogMemory();
			}
		}
	}
}