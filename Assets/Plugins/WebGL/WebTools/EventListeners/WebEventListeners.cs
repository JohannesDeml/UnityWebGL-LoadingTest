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
using System.Runtime.InteropServices;
using UnityEngine;

namespace Supyrb
{
	public class WebEventListeners : MonoBehaviour
	{

#if UNITY_WEBGL && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void _AddJsEventListener(string eventName);
#endif
		private const string GameObjectName = "WebEventListeners";

		private Dictionary<string, WebEventListener> eventListeners = new Dictionary<string, WebEventListener>();
		private static WebEventListeners instance;
#if UNITY_WEBGL
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			GameObject instanceGo = new GameObject(GameObjectName);
			instance = instanceGo.AddComponent<WebEventListeners>();
			DontDestroyOnLoad(instanceGo);
		}
#endif

		public static void AddEventListener(string eventName, Action callback)
		{
			instance.AddEventListenerInternal(eventName, callback);
		}

		private void AddEventListenerInternal(string eventName, Action callback)
		{
			if(eventListeners.TryGetValue(eventName, out var eventListener))
			{
				eventListener.OnEvent += callback;
			}
			else
			{
				var eventGo = new GameObject("WebEvent-" + eventName);
				eventGo.transform.parent = transform;
				var eventComponent = eventGo.AddComponent<WebEventListener>();
				eventListeners[eventName] = eventComponent;
				eventComponent.OnEvent += callback;

#if UNITY_WEBGL && !UNITY_EDITOR
				// Add event listener on javascript side
				_AddJsEventListener(eventName);
#elif UNITY_EDITOR && WEBTOOLS_LOG_CALLS
			Debug.Log($"<color=#00CCCC>{nameof(WebEventListeners)}.{nameof(AddEventListenerInternal)} add callback for {eventName}</color>");
#endif
			}
		}
	}
}