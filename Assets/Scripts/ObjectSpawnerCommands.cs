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
	public class ObjectSpawnerCommands : WebCommands
	{
		private ObjectSpawnController _objectSpawnController;
		private ObjectSpawnController ObjectSpawnController
		{
			get
			{
				if (_objectSpawnController == null)
				{
#if UNITY_2023_2_OR_NEWER
					_objectSpawnController = Object.FindFirstObjectByType<ObjectSpawnController>();
#else
					_objectSpawnController = Object.FindObjectOfType<ObjectSpawnController>();
#endif
				}

				return _objectSpawnController;
			}
		}

		[WebCommand(Description = "Pause spawning of cubes")]
		public void PauseSpawning()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.PauseSpawning();
			}
		}

		[WebCommand(Description = "Resume spawning of cubes")]
		public void ResumeSpawning()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.ResumeSpawning();
			}
		}

		[WebCommand(Description = "Add a spawner")]
		public void AddSpawner()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.AddSpawner();
			}
		}

		[WebCommand(Description = "Remove a spawner")]
		public void RemoveSpawner()
		{
			if (ObjectSpawnController != null)
			{
				ObjectSpawnController.RemoveSpawner();
			}
		}
	}
}