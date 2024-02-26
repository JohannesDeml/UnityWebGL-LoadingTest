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
#if UNITY_WEBGL
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
		private static extern uint _GetStaticMemorySize();
		[DllImport("__Internal")]
		private static extern uint _GetDynamicMemorySize();
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
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(AddFpsTrackingEvent)} called with {fps:0.00}");
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
		/// Log all current memory data in MB
		/// </summary>
		public static void LogMemory()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			var managed = GetManagedMemorySize();
			var native = GetNativeMemorySize();
			var total = GetTotalMemorySize();
			Debug.Log($"Memory stats:\nManaged: {managed:0.00}MB\nNative: {native:0.00}MB\nTotal: {total:0.00}MB");
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(LogMemory)} called");
#endif
		}

		/// <summary>
		/// Get the static memory size used by the application in MB
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetStaticMemorySize()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			var bytes = _GetStaticMemorySize();
			return GetMegaBytes(bytes);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(GetStaticMemorySize)} called");
			return -1f;
#else
			return -1f;
#endif
		}

		/// <summary>
		/// Get the dynamic memory size used by the application in MB
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetDynamicMemorySize()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			var bytes = _GetStaticMemorySize();
			return GetMegaBytes(bytes);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(GetDynamicMemorySize)} called");
			return -1f;
#else
			return -1f;
#endif
		}

		/// <summary>
		/// Get the native memory size used by the application in MB (Static + Dynamic memory)
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetNativeMemorySize()
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			return GetDynamicMemorySize() + GetStaticMemorySize();
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"{nameof(WebToolPlugins)}.{nameof(GetNativeMemorySize)} called");
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
	}
}