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
using System.Diagnostics;
using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// Tracks timing events for startup of the application
	/// Runs with early execution order so the timing
	/// is before other scripts with that event function
	/// </summary>
	[DefaultExecutionOrder(-100)]
	public class WebToolTimeTracker : MonoBehaviour
	{
		[SerializeField]
		private bool showInfoPanelByDefault = true;

		[SerializeField]
		private bool trackAwakeTime = true;

		[SerializeField]
		private bool trackStartTime = true;

		[Header("FPS Tracking")]
		[SerializeField]
		private bool trackFps = true;

		[SerializeField]
		private float updateInterval = 0.5f;

		private Stopwatch stopWatch;
		private int lastFrameCount;

		private void Awake()
		{
			if (showInfoPanelByDefault)
			{
				WebToolPlugins.ShowInfoPanel();
			}
			else
			{
				WebToolPlugins.HideInfoPanel();
			}

			if(trackFps)
			{
				WebToolPlugins.AddFpsTrackingEvent(0);
			}

			if (trackAwakeTime)
			{
				WebToolPlugins.AddTimeTrackingEvent("Awake");
			}

			stopWatch = Stopwatch.StartNew();
		}

		private void Start()
		{
			if (trackStartTime)
			{
				WebToolPlugins.AddTimeTrackingEvent("Start");
			}
		}

		private void Update()
		{
			if(!trackFps)
			{
				this.enabled = false;
				return;
			}

			if(stopWatch.Elapsed.TotalSeconds > updateInterval)
			{
				var currentFrameCount = Time.frameCount;
				var frameCount = currentFrameCount - lastFrameCount;
				float fps = (float)(frameCount / stopWatch.Elapsed.TotalSeconds);
				WebToolPlugins.AddFpsTrackingEvent(fps);
				stopWatch.Restart();
				lastFrameCount = currentFrameCount;
			}
		}

		private void OnApplicationPause(bool pauseStatus) {
			if (pauseStatus) {
				stopWatch.Stop();
			} else {
				stopWatch.Start();
			}
		}
	}
}