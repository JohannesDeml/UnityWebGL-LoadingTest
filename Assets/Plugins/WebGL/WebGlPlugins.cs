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
		private static extern void _LogStartTime(string startupTime, string unityVersion, string webGlVersion);

		[DllImport("__Internal")]
		private static extern void _LogMemoryInfo(uint native, uint managed, uint total);

		[DllImport("__Internal")]
		private static extern uint _GetTotalMemorySize();

		[DllImport("__Internal")]
		private static extern uint _GetStaticMemorySize();

		[DllImport("__Internal")]
		private static extern uint _GetDynamicMemorySize();

		/// <summary>
		/// Logs the start time for Unity and the time since the webpage started loading
		/// By default triggered by <see cref="WebGlLogger"/> in Awake with execution order -100
		/// Needs the Develop WebGL Template to include the webpage time information
		/// </summary>
		public static void LogStartTime()
		{
			#if UNITY_WEBGL && !UNITY_EDITOR
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

			_LogStartTime(Time.realtimeSinceStartup.ToString("0.00"), Application.unityVersion, webGl);
			#else
			Debug.Log("Not supported on this platform");
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
			#else
			Debug.Log("Not supported on this platform");
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
			Debug.Log("Not supported on this platform");
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
			Debug.Log("Not supported on this platform");
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
			Debug.Log("Not supported on this platform");
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
			Debug.Log("Not supported on this platform");
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