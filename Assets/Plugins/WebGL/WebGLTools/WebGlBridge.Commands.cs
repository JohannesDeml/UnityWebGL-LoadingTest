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
		/// Disable capturing all keyboard input, e.g. for using native html input fields
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","DisableCaptureAllKeyboardInput");</code>
		/// </summary>
		[WebGlCommand(Description = "Disable unity from consuming all keyboard input")]
		public void DisableCaptureAllKeyboardInput()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			WebGLInput.captureAllKeyboardInput = false;
			Debug.Log($"WebGLInput.captureAllKeyboardInput: {WebGLInput.captureAllKeyboardInput}");
#endif
		}
		
		/// <summary>
		/// Enable capturing all keyboard input, to make sure the game does not miss any key strokes
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","EnableCaptureAllKeyboardInput");</code>
		/// </summary>
		[WebGlCommand(Description = "Enable unity from consuming all keyboard input")]
		public void EnableCaptureAllKeyboardInput()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			WebGLInput.captureAllKeyboardInput = true;
			Debug.Log($"WebGLInput.captureAllKeyboardInput: {WebGLInput.captureAllKeyboardInput}");
#endif
		}
		
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
		
		/// <summary>
		/// Log an example message to see if it is rendered correctly, and to see the stacktrace
		/// </summary>
		[WebGlCommand(Description = "Log an example debug message")]
		public void LogExampleMessage()
		{
			Debug.Log("This is an <color=#ff0000>example</color> message, showing off <color=#ff00ff>rich text</color> support!");
		}
		
		/// <summary>
		/// Log a custom message to test Debug.Log in general
		/// </summary>
		/// <param name="message">Message that will be logged</param>
		[WebGlCommand(Description = "Log a custom message")]
		public void LogMessage(string message)
		{
			Debug.Log(message);
		}
	} 
}