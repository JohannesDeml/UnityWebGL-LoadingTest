// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlBridge.Commands.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb.Attributes;
using UnityEngine;

namespace Supyrb
{
	public partial class WebGlBridge
	{
		/// <summary>
		/// Logs the current memory usage
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","LogMemory");</code>
		/// </summary>
		[WebGlCommand(Description = "Logs the current memory")]
		public void LogMemory()
		{
			WebGlPlugins.LogMemory();
		}
		
		/// <summary>
		/// Sets if the application should run while not focused.
		/// It is in background while another tab is focused or the console is focused
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetApplicationRunInBackground", 1);</code>
		/// </summary>
		/// <param name="runInBackground">1 if it should run in background</param>
		[WebGlCommand(Description = "Application.runInBackground")]
		public void SetApplicationRunInBackground(int runInBackground)
		{
			Application.runInBackground = runInBackground == 1;
		}
		
		/// <summary>
		/// Sets the rendering frame rate, see <see cref="Application.targetFrameRate"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetApplicationTargetFrameRate", 15);</code>
		/// </summary>
		/// <param name="targetFrameRate">frame rate to render in</param>
		[WebGlCommand(Description = "Application.targetFrameRate")]
		public void SetApplicationTargetFrameRate(int targetFrameRate)
		{
			Application.targetFrameRate = targetFrameRate;
		}
		
		/// <summary>
		/// Sets the interval for fixed time updates, see <see cref="Time.fixedDeltaTime"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetTimeFixedDeltaTime", 0.02);</code>
		/// </summary>
		/// <param name="fixedDeltaTime"></param>
		[WebGlCommand(Description = "Time.fixedDeltaTime")]
		public void SetTimeFixedDeltaTime(float fixedDeltaTime)
		{
			Time.fixedDeltaTime = fixedDeltaTime;
		}
		
		/// <summary>
		/// Sets the global timeScale, see <see cref="Time.timeScale"/>
		/// Somehow the timescale also affects the physics refresh rate on WebGL
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetTimeTimeScale", 0.2);</code>
		/// </summary>
		/// <param name="timeScale">new timescale value</param>
		[WebGlCommand(Description = "Time.timeScale")]
		public void SetTimeTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}
		
		/// <summary>
		/// Toggle the visibility of the info panel in the top right corner
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "ToggleInfoPanel");</code>
		/// </summary>
		[WebGlCommand(Description = "Toggle develop ui visibility of InfoPanel")]
		public void ToggleInfoPanel()
		{
			WebGlPlugins.ToggleInfoPanel();
		}
	} 
}