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
	/// <summary>
	/// Spawns an object in regular intervals
	/// </summary>
	public class ObjectSpawner : MonoBehaviour
	{
		[SerializeField]
		private GameObject prefab = null;

		[SerializeField]
		private float spawnCooldownSeconds = 0.5f;

		[SerializeField]
		private int maxInstances = 200;

		private int instances = 0;
		private Queue<GameObject> spawnedObjects = null;
		private float lastSpawnTime;

		private void Awake()
		{
			spawnedObjects = new Queue<GameObject>(maxInstances + 5);
			lastSpawnTime = Time.time;
		}

		private void Update()
		{
			if (lastSpawnTime + spawnCooldownSeconds <= Time.time)
			{
				SpawnObject();
				lastSpawnTime = Time.time;
			}
		}

		private void SpawnObject()
		{
			if (instances >= maxInstances)
			{
				var recycleGo = spawnedObjects.Dequeue();
				recycleGo.transform.localPosition = transform.position;
				recycleGo.transform.localRotation = transform.localRotation;
				spawnedObjects.Enqueue(recycleGo);
				return;
			}

			var newGo = Instantiate(prefab, transform.position, transform.rotation);
			spawnedObjects.Enqueue(newGo);
			instances++;
		}

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, 0.5f);
		}
		#endif
	}
}