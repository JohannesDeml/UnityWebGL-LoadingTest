// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectSpawner.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Supyrb
{
	public class ObjectSpawnController : MonoBehaviour
	{
		[SerializeField]
		private ObjectSpawner objectSpawnerPrefab = null;

		[SerializeField]
		private int spawnerCount = 4;

		[SerializeField]
		private float spawnerRadius = 0.5f;

		[SerializeField]
		private float spawnerCoolDown = 0.5f;

		private List<ObjectSpawner> spawners;

		private void Awake()
		{
			spawners = new List<ObjectSpawner>();
			for (int i = 0; i < spawnerCount; i++)
			{
				var spawner = Instantiate(objectSpawnerPrefab, transform);
				spawners.Add(spawner);
			}

			UpdateSpawners();
		}

		[ContextMenu("Add Spawner")]
		public void AddSpawner()
		{
			var spawner = Instantiate(objectSpawnerPrefab, transform);
			spawners.Add(spawner);
			UpdateSpawners();
		}

		[ContextMenu("Remove Spawner")]
		public void RemoveSpawner()
		{
			if (spawners.Count == 0)
			{
				return;
			}

			var spawner = spawners[spawners.Count - 1];
			spawners.RemoveAt(spawners.Count - 1);
			Destroy(spawner.gameObject);
			UpdateSpawners();
		}

		[ContextMenu("Pause Spawning")]
		public void PauseSpawning()
		{
			foreach (var spawner in spawners)
			{
				spawner.PauseSpawning();
			}
		}

		[ContextMenu("Resume Spawning")]
		public void ResumeSpawning()
		{
			foreach (var spawner in spawners)
			{
				spawner.ResumeSpawning();
			}
		}

		private void UpdateSpawners()
		{
			int count = spawners.Count;
			float angleStep = 360f / count;
			float coolDownStep = spawnerCoolDown / count;
			for (int i = 0; i < count; i++)
			{
				var spawner = spawners[i];
				float angle = i * angleStep;
				Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * spawnerRadius;
				spawner.transform.localPosition = position;
				spawner.SpawnCoolDownSeconds = spawnerCoolDown;
				spawner.SpawnOffsetSeconds = i * coolDownStep;
			}
		}
	}
}