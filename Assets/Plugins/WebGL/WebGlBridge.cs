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
using System.Reflection;
using System.Text;
using Supyrb.Attributes;
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

		[ContextMenu("Log all commands")]
		[WebGlCommand(Description = "Log all available commands")]
		public void Help()
		{
			StringBuilder sb = new StringBuilder();
			MethodInfo[] methods = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

			sb.AppendLine("Available unity interfaces:");
			for (int i = 0; i < methods.Length; i++)
			{
				var method = methods[i];
				WebGlCommandAttribute commandAttribute = method.GetCustomAttribute<WebGlCommandAttribute>();
				if (commandAttribute != null)
				{
					sb.Append($"- {method.Name}(");
					ParameterInfo[] parameters = method.GetParameters();
					for (int j = 0; j < parameters.Length; j++)
					{
						var parameter = parameters[j];
						sb.Append($"{parameter.ParameterType} {parameter.Name}");
						if (j < parameters.Length - 1)
						{
							sb.Append(", ");
						}
					}

					sb.AppendLine($") -> {commandAttribute.Description}");
				}
			}

			sb.AppendLine("\nRun a command with 'unityGame.SendMessage(\"WebGL\", \"COMMAND_NAME\",PARAMETER)'");
			Debug.Log(sb.ToString());
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
		/// </summary>
		[WebGlCommand(Description = "Toggle develop ui visibility of InfoPanel")]
		public void ToggleInfoPanel()
		{
			WebGlPlugins.ToggleInfoPanel();
		}
	}
}