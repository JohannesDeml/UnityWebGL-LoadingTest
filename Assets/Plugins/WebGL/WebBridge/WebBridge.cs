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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Supyrb.Attributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace Supyrb
{
	/// <summary>
	/// Bridge to Unity to access unity logic through the browser console
	/// You can extend your commands by creating a partial class for WebBridge, see WebBridge.Commands as an example
	/// </summary>
	public class WebBridge : WebCommands
	{
		private const string WebBridgeGameObjectName = "WebBridge";

		private static GameObject bridgeInstance;
#if UNITY_WEBGL
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			SetGlobalVariables();
			bridgeInstance = new GameObject(WebBridgeGameObjectName);
			DontDestroyOnLoad(bridgeInstance);
			AddAllWebCommands();
		}
#endif

		private static void AddAllWebCommands()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> webCommandTypes = new List<Type>();
			foreach (var assembly in assemblies)
			{
				Type[] types = assembly.GetTypes();
				foreach (var type in types)
				{
					if (type.IsSubclassOf(typeof(WebCommands)))
					{
						webCommandTypes.Add(type);
					}
				}
			}

			// Sort the commands to make the output deterministic
			webCommandTypes.Sort((a, b) => a.Name.CompareTo(b.Name));
			foreach (var webCommandType in webCommandTypes)
			{
				bridgeInstance.AddComponent(webCommandType);
			}
		}

		private static void SetGlobalVariables()
		{
			var graphicsDevice = SystemInfo.graphicsDeviceType;
			string webGraphics = string.Empty;
			switch (graphicsDevice)
			{
				case GraphicsDeviceType.OpenGLES2:
					webGraphics = "WebGL 1";
					break;
				case GraphicsDeviceType.OpenGLES3:
					webGraphics = "WebGL 2";
					break;
#if UNITY_2023_2_OR_NEWER
				case GraphicsDeviceType.WebGPU:
					webGraphics = "WebGPU";
					break;
#endif
				default:
					webGraphics = graphicsDevice.ToString();
					break;
			}
			WebToolPlugins.SetVariable("webGlVersion", webGraphics);
			WebToolPlugins.SetVariable("unityVersion", Application.unityVersion);
			WebToolPlugins.SetVariable("applicationVersion", Application.version);
#if !UNITY_EDITOR && UNITY_WEBGL
			WebToolPlugins.SetVariable("unityCaptureAllKeyboardInputDefault", WebGLInput.captureAllKeyboardInput?"true":"false");
#endif
		}

		private void Start()
		{
#if (!UNITY_EDITOR && UNITY_WEBGL) || UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"Unity WebGL Bridge ready -> Run 'runUnityCommand(\"Help\")' in the browser console to see usage");
#endif
		}

		[WebCommand(Description = "Log all available commands")]
		[ContextMenu(nameof(Help))]
		public void Help()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Available commands:");

			foreach (var webCommand in gameObject.GetComponents<WebCommands>())
			{
				sb.AppendLine($"<b>---{webCommand.GetType().Name}---</b>");
				MethodInfo[] methods = webCommand.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

				for (int i = 0; i < methods.Length; i++)
				{
					var method = methods[i];
					WebCommandAttribute commandAttribute = method.GetCustomAttribute<WebCommandAttribute>();
					if (commandAttribute != null)
					{
						sb.Append($"runUnityCommand(\"{method.Name}\"");
						ParameterInfo[] parameters = method.GetParameters();
						for (int j = 0; j < parameters.Length; j++)
						{
							var parameter = parameters[j];
							if (parameter.ParameterType == typeof(string))
							{
								sb.Append($", \"{parameter.ParameterType} {parameter.Name}\"");
							}
							else
							{
								sb.Append($", {parameter.ParameterType} {parameter.Name}");
							}
						}

						sb.AppendLine($"); <color=#555555FF>-> {commandAttribute.Description}</color>");
					}
				}

			}

			sb.AppendLine($"\nRun a command with 'runUnityCommand(\"COMMAND_NAME\",PARAMETER);'");
			Debug.Log(sb.ToString());
		}
	}
}