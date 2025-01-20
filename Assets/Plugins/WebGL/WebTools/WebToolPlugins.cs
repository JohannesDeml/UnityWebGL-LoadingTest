// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlPlugins.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using UnityEngine.Rendering;
#endif

namespace Supyrb
{
	public static class WebToolPlugins
	{
#if UNITY_WEBGL && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void _SetStringVariable(string variableName, string variableValue);
		[DllImport("__Internal")]
		private static extern void _AddTimeTrackingEvent(string eventName);
		[DllImport("__Internal")]
		private static extern void _AddFpsTrackingEvent(float fps);
		[DllImport("__Internal")]
		private static extern void _ShowInfoPanel();
		[DllImport("__Internal")]
		private static extern void _HideInfoPanel();
		[DllImport("__Internal")]
		private static extern string _GetUserAgent();
		[DllImport("__Internal")]
		private static extern uint _GetTotalMemorySize();
		[DllImport("__Internal")]
		private static extern bool _CopyToClipboard(string text);
		[DllImport("__Internal")]
		private static extern int _IsOnline();
		[DllImport("__Internal")]
		private static extern void _DownloadFile(string filename, string content);
		[DllImport("__Internal")]
		private static extern void _DownloadBlob(string filename, byte[] byteArray, int byteLength, string mimeType);

#endif

		private static bool _infoPanelVisible = false;

		/// <summary>
		/// Sets a global string variable in JavaScript.
		/// This variable can be accessed by the console and other javascript functions
		/// </summary>
		/// <param name="variableName">Name of the variable</param>
		/// <param name="value">Value of the variable</param>
		public static void SetVariable(string variableName, string value)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			_SetStringVariable(variableName, value);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"<color=#00CCCC>{nameof(WebToolPlugins)}.{nameof(SetVariable)} set {variableName}: {value}</color>");
#endif
		}

		/// <summary>
		/// Adds a time marker at the call time to the javascript map "unityTimeTrackers"
		/// The mapped value is performance.now() at the time of the call
		/// </summary>
		/// <param name="eventName">Name of the tracking event, e.g. "Awake"</param>
		public static void AddTimeTrackingEvent(string eventName)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			_AddTimeTrackingEvent(eventName);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(AddTimeTrackingEvent)} called with {eventName}");
#endif
		}

		public static void AddFpsTrackingEvent(float fps)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			_AddFpsTrackingEvent(fps);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			// This is called often, so it can spam the console, uncomment if needed
			//Debug.Log($"{nameof(WebToolPlugins)}.{nameof(AddFpsTrackingEvent)} called with {fps:0.00}");
#endif
		}

		/// <summary>
		/// Show the info panel in the top right corner
		/// By default triggered by <see cref="WebGlTimeTracker"/> in Awake
		/// Needs the Develop WebGL Template to provide the logic
		/// </summary>
		public static void ShowInfoPanel()
		{
			_infoPanelVisible = true;
#if UNITY_WEBGL && !UNITY_EDITOR
			_ShowInfoPanel();
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(ShowInfoPanel)} called");
#endif
		}

		/// <summary>
		/// Hide the info panel in the top right corner
		/// By default triggered by <see cref="WebGlTimeTracker"/> in Awake
		/// Needs the Develop WebGL Template to provide the logic
		/// </summary>
		public static void HideInfoPanel()
		{
			_infoPanelVisible = false;
#if UNITY_WEBGL && !UNITY_EDITOR
			_HideInfoPanel();
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(HideInfoPanel)} called");
#endif
		}

		/// <summary>
		/// Toggle the visibility of the info panel
		/// </summary>
		public static void ToggleInfoPanel()
		{
			if (_infoPanelVisible)
			{
				HideInfoPanel();
			}
			else
			{
				ShowInfoPanel();
			}
		}

		/// <summary>
		/// Get navigator.userAgent from the browser
		/// </summary>
		/// <returns>navigator.userAgent</returns>
		public static string GetUserAgent()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			return _GetUserAgent();
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(GetUserAgent)} called");
			return "undefined";
#else
			return "undefined";
#endif
		}

		/// <summary>
		/// Check user agent to determine if the game runs on a mobile device
		/// </summary>
		/// <returns>true if on phone or tablet</returns>
		public static bool IsMobileDevice()
		{
			string userAgent = GetUserAgent();
			return userAgent.Contains("iPhone") ||
				userAgent.Contains("iPad") ||
				userAgent.Contains("iPod") ||
				userAgent.Contains("Android");
		}

		/// <summary>
		/// Log all current memory data in MB
		/// </summary>
		public static void LogMemory()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			var managed = GetManagedMemorySize();
			var total = GetTotalMemorySize();
			Debug.Log($"Memory stats:\nManaged: {managed:0.00}MB\nTotal: {total:0.00}MB");
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(LogMemory)} called");
#endif
		}

		/// <summary>
		/// Get the total memory size used by the application in MB
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetTotalMemorySize()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			var bytes = _GetTotalMemorySize();
			return GetMegaBytes(bytes);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(GetTotalMemorySize)} called");
			return -1f;
#else
			return -1f;
#endif
		}

		/// <summary>
		/// Get the managed memory size used by the application in MB
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetManagedMemorySize()
		{
			var bytes = (uint)GC.GetTotalMemory(false);
			return GetMegaBytes(bytes);
		}

		/// <summary>
		/// Converts bytes (B) to mega bytes (MB)
		/// </summary>
		/// <param name="bytes">bytes to convert</param>
		/// <returns>bytes / (1024 * 1024)</returns>
		private static float GetMegaBytes(uint bytes)
		{
			return (float)bytes / (1024 * 1024);
		}

		/// <summary>
		/// Copies the specified text to the system clipboard using the browser's clipboard API.
		/// Only works in WebGL builds and requires clipboard-write permission in modern browsers.
		/// </summary>
		/// <param name="text">The text to copy to the clipboard</param>
		/// <returns>True if the copy operation was successful, false otherwise</returns>
		public static void CopyToClipboard(string text)
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
				_CopyToClipboard(text);
			#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
				Debug.Log($"{nameof(WebToolPlugins)}.{nameof(CopyToClipboard)} called with: {text}");
			#endif
		}

		/// <summary>
		/// Checks if the browser currently has an internet connection using the navigator.onLine property.
		/// </summary>
		/// <returns>True if the browser is online, false if it's offline</returns>
		public static bool IsOnline()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
				return _IsOnline() == 1;
			#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
				Debug.Log($"{nameof(WebToolPlugins)}.{nameof(IsOnline)} called");
				return true;
			#else
				return true;
			#endif
		}

		/// <summary>
		/// Downloads a text file through the browser with the specified filename and content.
		/// Creates a temporary anchor element to trigger the download.
		/// </summary>
		/// <param name="filename">The name of the file to be downloaded</param>
		/// <param name="content">The text content to be saved in the file</param>
		public static void DownloadTextFile(string filename, string content)
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
				_DownloadFile(filename, content);
			#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
				Debug.Log($"{nameof(WebToolPlugins)}.{nameof(DownloadTextFile)} called with filename: {filename}");
			#endif
		}

		/// <summary>
		/// Downloads a binary file through the browser with the specified filename and data.
		/// Creates a Blob with the specified MIME type and triggers the download.
		/// </summary>
		/// <param name="filename">The name of the file to be downloaded</param>
		/// <param name="data">The binary data to be saved in the file</param>
		/// <param name="mimeType">The MIME type of the file (defaults to "application/octet-stream")</param>
		/// <example>
		/// <code>
		/// // Example: Save a Texture2D as PNG
		/// Texture2D texture;
		/// byte[] pngData = texture.EncodeToPNG();
		/// WebToolPlugins.DownloadBinaryFile("texture.png", pngData, "image/png");
		/// </code>
		/// </example>
		public static void DownloadBinaryFile(string filename, byte[] data, string mimeType = "application/octet-stream")
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
				_DownloadBlob(filename, data, data.Length, mimeType);
			#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
				Debug.Log($"{nameof(WebToolPlugins)}.{nameof(DownloadBinaryFile)} called with filename: {filename}");
			#endif
		}
	}
}