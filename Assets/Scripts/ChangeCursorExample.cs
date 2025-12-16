// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeCursorExample.cs">
//   Copyright (c) 2025 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public class ChangeCursorExample : MonoBehaviour
	{
		public string CursorEnteredName = "pointer";
		public string CursorExitedName = "default";
		public bool DestroyOnClick = true;

		private void OnMouseEnter()
		{
			WebToolPlugins.SetCursor(CursorEnteredName);
		}

		private void OnMouseExit()
		{
			WebToolPlugins.SetCursor(CursorExitedName);
		}

		private void OnMouseDown()
		{
			if(DestroyOnClick)
			{
				Destroy(gameObject);
			}
		}
	}
}