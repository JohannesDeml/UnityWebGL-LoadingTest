// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebEventListener.cs">
//   Copyright (c) 2024 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Supyrb
{
    public class WebEventListener : MonoBehaviour
	{
		public event Action OnEvent;

		// Called from JavaScript
		public void TriggerEvent()
		{
			OnEvent?.Invoke();
		}

		private void OnDestroy()
		{
			// Clean up event handlers when destroyed
			OnEvent = null;
		}
	}
}