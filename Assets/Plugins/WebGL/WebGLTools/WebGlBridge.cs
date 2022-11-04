// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlBridge.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

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
	public partial class WebGlBridge : MonoBehaviour
	{
#if UNITY_WEBGL
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			SetGlobalVariables();
			var bridgeInstance = new GameObject("WebGL");
			DontDestroyOnLoad(bridgeInstance);
			bridgeInstance.AddComponent<WebGlBridge>();
		}
#endif

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
	}
}