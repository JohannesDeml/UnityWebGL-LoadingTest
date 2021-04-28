// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadingTime.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using UnityEngine;

namespace Supyrb
{
	public class LoadingTime : MonoBehaviour
	{
		[DllImport("__Internal")]
		private static extern void LogStartTime(string text);
		
		[SerializeField]
		private bool logStartTime = true;

		private void Start()
		{
			if (logStartTime)
			{
				LogStartTime(Time.realtimeSinceStartup.ToString("0.00"));
			}
		}
	}
}