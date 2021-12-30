// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadingTime.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// Tracks timing events for startup of the application
	/// Runs with early execution order so the timing
	/// is before other scripts with that event function
	/// </summary>
	[DefaultExecutionOrder(-100)]
	public class WebGlTimeTracker : MonoBehaviour
	{
		[SerializeField] 
		private bool showInfoPanelByDefault = true;
		
		[SerializeField]
		private bool trackAwakeTime = true;
		
		[SerializeField]
		private bool trackStartTime = true;

		private void Awake()
		{
			if (showInfoPanelByDefault)
			{
				WebGlPlugins.ShowInfoPanel();
			}
			else
			{
				WebGlPlugins.HideInfoPanel();
			}
			
			if (trackAwakeTime)
			{
				WebGlPlugins.AddTimeTrackingEvent("Awake");
			}
		}

		private void Start()
		{
			if (trackStartTime)
			{
				WebGlPlugins.AddTimeTrackingEvent("Start");
			}
		}
	}
}