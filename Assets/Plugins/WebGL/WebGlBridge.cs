// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlBridge.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Supyrb
{
	/// <summary>
	/// Bridge to Unity to access unity logic through the browser console
	/// </summary>
	public class WebGlBridge : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			SetGlobalVariables();
			var bridgeInstance = new GameObject("WebGL");
			DontDestroyOnLoad(bridgeInstance);
			bridgeInstance.AddComponent<WebGlBridge>();
		}

		private static void SetGlobalVariables()
		{
			var graphicsDevice = SystemInfo.graphicsDeviceType;
			string webGl = string.Empty;
			switch (graphicsDevice)
			{
				case GraphicsDeviceType.OpenGLES2:
					webGl = "WebGL 1";
					break;
				case GraphicsDeviceType.OpenGLES3:
					webGl = "WebGL 2";
					break;
				default:
					webGl = graphicsDevice.ToString();
					break;
			}
			WebGlPlugins.SetVariable("webGlVersion", webGl);
			WebGlPlugins.SetVariable("unityVersion", Application.unityVersion);
		}

		private void Start()
		{
			Debug.Log("Unity WebGL Bridge ready -> Run 'unityGame.SendMessage(\"WebGL\", \"Help\")' in the browser console to see usage");
		}

		public void Help()
		{
			Debug.Log("Available unity interfaces:\n" +
			          "- LogMemory() -> logs current memory\n" +
			          "- SetApplicationRunInBackground(int runInBackground) -> Application.runInBackground\n" +
			          "- SetApplicationTargetFrameRate(int targetFrameRate) -> Application.targetFrameRate\n" +
			          "- SetTimeFixedDeltaTime(float fixedDeltaTime) -> Time.fixedDeltaTime\n" +
			          "- SetTimeTimeScale(float timeScale) -> Time.timeScale\n" +
			          "- ToggleInfoPanel() -> Toggle develop ui visibility of InfoPanel\n" +
			          "\nRun a command through 'unityGame.SendMessage(\"WebGL\", \"COMMAND_NAME\",PARAMETER)'");
		}

		/// <summary>
		/// Logs the current memory usage
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","LogMemory");</code>
		/// </summary>
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
		public void SetApplicationRunInBackground(int runInBackground)
		{
			Application.runInBackground = runInBackground == 1;
		}
		
		/// <summary>
		/// Sets the rendering frame rate, see <see cref="Application.targetFrameRate"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetApplicationTargetFrameRate", 15);</code>
		/// </summary>
		/// <param name="targetFrameRate">frame rate to render in</param>
		public void SetApplicationTargetFrameRate(int targetFrameRate)
		{
			Application.targetFrameRate = targetFrameRate;
		}
		
		/// <summary>
		/// Sets the interval for fixed time updates, see <see cref="Time.fixedDeltaTime"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetTimeFixedDeltaTime", 0.02);</code>
		/// </summary>
		/// <param name="fixedDeltaTime"></param>
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
		public void SetTimeTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}
		
		/// <summary>
		/// Toggle the visibility of the info panel in the top right corner
		/// </summary>
		public void ToggleInfoPanel()
		{
			WebGlPlugins.ToggleInfoPanel();
		}
	}
}