// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectSpawner.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb.Attributes;
using UnityEngine;

namespace Supyrb
{
    /// <summary>
    /// Browser commands for spawning cubes
    /// </summary>
    public partial class WebBridge
	{
		private ObjectSpawnController _objectSpawnController;
		private ObjectSpawnController ObjectSpawnController
		{
			get
			{
				if (_objectSpawnController == null)
				{
					_objectSpawnController = Object.FindObjectOfType<ObjectSpawnController>();
				}

				return _objectSpawnController;
			}
		}

		[WebGlCommand(Description = "Pause spawning of cubes")]
		public void PauseSpawning()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.PauseSpawning();
			}
		}

		[WebGlCommand(Description = "Resume spawning of cubes")]
		public void ResumeSpawning()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.ResumeSpawning();
			}
		}

		[WebGlCommand(Description = "Add a spawner")]
		public void AddSpawner()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.AddSpawner();
			}
		}

		[WebGlCommand(Description = "Remove a spawner")]
		public void RemoveSpawner()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.RemoveSpawner();
			}
		}
	}
}