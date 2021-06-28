// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadingTime.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public class WebGlLogger : MonoBehaviour
	{
		[SerializeField]
		private bool logStartTime = true;

		[SerializeField]
		private bool logMemory = true;

		private void Start()
		{
			if (logStartTime)
			{
				WebGlPlugins.LogStartTime();
			}

			if (logMemory)
			{
				WebGlPlugins.LogMemory();
			}
		}
	}
}