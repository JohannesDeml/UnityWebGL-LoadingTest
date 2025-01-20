// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebEventListener.cs">
//   Copyright (c) 2024 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
    public class CommonWebEventListener : MonoBehaviour
	{
		private void Start()
		{
			WebEventListeners.AddEventListener("focus", () =>
			{
				Debug.Log("WebEvent focus triggered");
			});

			WebEventListeners.AddEventListener("blur", () =>
			{
				Debug.Log("WebEvent blur triggered");
			});
		}
	}
}