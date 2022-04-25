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
	public static class WebGlPlugins
	{
		[DllImport("__Internal")]
		private static extern void _SetStringVariable(string variableName, string variableValue);
		[DllImport("__Internal")]
		private static extern void _AddTimeTrackingEvent(string eventName);
		[DllImport("__Internal")]
		private static extern void _ShowInfoPanel();
		[DllImport("__Internal")]
		private static extern void _HideInfoPanel();

		[DllImport("__Internal")]
		private static extern void _LogMemoryInfo(uint native, uint managed, uint total);

		[DllImport("__Internal")]
		private static extern uint _GetTotalMemorySize();

		[DllImport("__Internal")]
		private static extern uint _GetStaticMemorySize();

		[DllImport("__Internal")]
		private static extern uint _GetDynamicMemorySize();

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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(SetVariable)} called with {variableName}: {value}");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(AddTimeTrackingEvent)} called with {eventName}");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(ShowInfoPanel)} called");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(HideInfoPanel)} called");
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
		/// Log all current memory data in MB
		/// </summary>
		public static void LogMemory()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
			var managed = GetManagedMemorySize();
			var native = GetNativeMemorySize();
			var total = GetTotalMemorySize();
			Debug.Log($"Memory stats:\nManaged: {managed:0.00}MB\nNative: {native:0.00}MB\nTotal: {total:0.00}MB");
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(LogMemory)} called");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(GetTotalMemorySize)} called");
			return -1f;
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(GetStaticMemorySize)} called");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(GetDynamicMemorySize)} called");
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
			#else
			Debug.Log($"{nameof(WebGlPlugins)}.{nameof(GetNativeMemorySize)} called");
			return -1f;
			#endif
		}

		/// <summary>
		/// Get the managed memory size used by the application in MB
		/// </summary>
		/// <returns>Size in MB</returns>
		public static float GetManagedMemorySize()
		{
			var bytes = (uint) GC.GetTotalMemory(false);
			return GetMegaBytes(bytes);
		}
		
		/// <summary>
		/// Converts bytes (B) to mega bytes (MB)
		/// </summary>
		/// <param name="bytes">bytes to convert</param>
		/// <returns>bytes / (1024 * 1024)</returns>
		private static float GetMegaBytes(uint bytes)
		{
			return (float) bytes / (1024 * 1024);
		}
	}
}