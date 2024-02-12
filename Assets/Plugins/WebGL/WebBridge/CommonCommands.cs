// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlBridge.Commands.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Supyrb.Attributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace Supyrb
{
	/// <summary>
	/// Add commands to the WebGL bridge to expose them to the browser console
	/// </summary>
	public class CommonCommands : WebCommands
	{
		/// <summary>
		/// Disable capturing all keyboard input, e.g. for using native html input fields
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","DisableCaptureAllKeyboardInput");</code>
		/// </summary>
		[WebCommand(Description = "Disable unity from consuming all keyboard input")]
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
		[WebCommand(Description = "Enable unity from consuming all keyboard input")]
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
		[WebCommand(Description = "Logs the current memory")]
		[ContextMenu(nameof(LogMemory))]
		public void LogMemory()
		{
			WebToolPlugins.LogMemory();
		}

		/// <summary>
		/// Unloads all unused assets <see cref="Resources.UnloadUnusedAssets"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL","UnloadUnusedAssets");</code>
		/// </summary>
		[WebCommand(Description = "Resources.UnloadUnusedAssets")]
		[ContextMenu(nameof(UnloadUnusedAssets))]
		public void UnloadUnusedAssets()
		{
			Resources.UnloadUnusedAssets();
		}

		/// <summary>
		/// Sets if the application should run while not focused.
		/// It is in background while another tab is focused or the console is focused
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetApplicationRunInBackground", 1);</code>
		/// </summary>
		/// <param name="runInBackground">1 if it should run in background</param>
		[WebCommand(Description = "Application.runInBackground")]
		public void SetApplicationRunInBackground(int runInBackground)
		{
			Application.runInBackground = runInBackground == 1;
		}

		/// <summary>
		/// Sets the rendering frame rate, see <see cref="Application.targetFrameRate"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetApplicationTargetFrameRate", 15);</code>
		/// </summary>
		/// <param name="targetFrameRate">frame rate to render in</param>
		[WebCommand(Description = "Application.targetFrameRate")]
		public void SetApplicationTargetFrameRate(int targetFrameRate)
		{
			Application.targetFrameRate = targetFrameRate;
		}

		/// <summary>
		/// Sets the interval for fixed time updates, see <see cref="Time.fixedDeltaTime"/>
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "SetTimeFixedDeltaTime", 0.02);</code>
		/// </summary>
		/// <param name="fixedDeltaTime"></param>
		[WebCommand(Description = "Time.fixedDeltaTime")]
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
		[WebCommand(Description = "Time.timeScale")]
		public void SetTimeTimeScale(float timeScale)
		{
			Time.timeScale = timeScale;
		}

		/// <summary>
		/// Toggle the visibility of the info panel in the top right corner
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "ToggleInfoPanel");</code>
		/// </summary>
		[WebCommand(Description = "Toggle develop ui visibility of InfoPanel")]
		public void ToggleInfoPanel()
		{
			WebToolPlugins.ToggleInfoPanel();
		}

		/// <summary>
		/// Log the user agent of the browser and if this agent is classified as mobile
		/// Browser Usage: <code>unityGame.SendMessage("WebGL", "LogUserAgent");</code>
		/// </summary>
		[WebCommand(Description = "Log User Agent and isMobileDevice")]
		public void LogUserAgent()
		{
			string userAgent = WebToolPlugins.GetUserAgent();
			bool isMobileDevice = WebToolPlugins.IsMobileDevice();
			Debug.Log($"<color=#4D65A4>User Agent:</color> '{userAgent}', <color=#4D65A4>IsMobileDevice:</color> '{isMobileDevice}'");
		}

		/// <summary>
		/// Log example messages to show off unity rich text parsing to html & console styling
		/// </summary>
		[WebCommand(Description = "Log example messages for Log, warning and error")]
		[ContextMenu(nameof(LogExampleMessages))]
		public void LogExampleMessages()
		{
			Debug.Log("Example unity rich text example with <color=red>red and <color=blue>nested blue</color> color tags</color> and <b>bold</b> + <i>italic</i> tags, and <size=20>custom size with <b><i>bold italic nesting</i></b></size> tag.");
			Debug.LogWarning("This is an <b>example <color=#e28743>warning</color></b> message!");
			Debug.LogError("This is an <b>example <color=#b50808>error</color></b> message!");
		}

		/// <summary>
		/// Log a custom message to test Debug.Log in general
		/// </summary>
		/// <param name="message">Message that will be logged</param>
		[WebCommand(Description = "Log a custom message")]
		public void LogMessage(string message)
		{
			Debug.Log(message);
		}

		/// <summary>
		/// Throw an exception from System namespace to see how stack traces look for that
		/// </summary>
		[WebCommand(Description = "Throw a dictionary key not found exception")]
		[ContextMenu(nameof(ThrowDictionaryException))]
		public void ThrowDictionaryException()
		{
			Dictionary<int, int> testDictionary = new Dictionary<int, int>();
			// this will throw a KeyNotFoundException because the key does not exist
			Debug.Log(testDictionary[10]);
		}

		/// <summary>
		/// Log information of all texture formats that Unity supports, which ones are supported by
		/// the current platform and browser, and which ones are not supported
		/// </summary>
		[WebCommand(Description = "Log supported and unsupported texture formats")]
		[ContextMenu(nameof(LogTextureSupport))]
		public void LogTextureSupport()
		{
			List<TextureFormat> supportedFormats = new List<TextureFormat>();
			List<TextureFormat> unsupportedFormats = new List<TextureFormat>();

			foreach (TextureFormat textureFormat in Enum.GetValues(typeof(TextureFormat)))
			{
				var memberInfos = typeof(TextureFormat).GetMember(textureFormat.ToString());
				object[] obsoleteAttributes = memberInfos[0].GetCustomAttributes(typeof(ObsoleteAttribute), false);
				if (obsoleteAttributes.Length > 0)
				{
					// don't evaluate obsolete enum values
					continue;
				}

				if (SystemInfo.SupportsTextureFormat(textureFormat))
				{
					supportedFormats.Add(textureFormat);
				}
				else
				{
					unsupportedFormats.Add(textureFormat);
				}
			}

			Debug.Log($"Supported Texture formats: \n{string.Join(", ", supportedFormats)}");
			Debug.Log($"Unsupported Texture formats: \n{string.Join(", ", unsupportedFormats)}");
		}

		/// <summary>
		/// Deletes all player prefs <see cref="PlayerPrefs.DeleteAll"/>
		/// </summary>
		[WebCommand(Description = "PlayerPrefs.DeleteAll")]
		[ContextMenu(nameof(DeleteAllPlayerPrefs))]
		public void DeleteAllPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
		}

		/// <summary>
		/// Log shader compilation in debug builds (no effect in release builds)
		/// <see cref="GraphicsSettings.logWhenShaderIsCompiled "/>
		/// </summary>
		/// <param name="runInBackground">1 if it should run in background</param>
		[WebCommand(Description = "GraphicsSettings.logWhenShaderIsCompiled")]
		[ContextMenu(nameof(LogShaderCompilation))]
		public void LogShaderCompilation(int enabled)
		{
			GraphicsSettings.logWhenShaderIsCompiled = enabled == 1;
		}
	}
}