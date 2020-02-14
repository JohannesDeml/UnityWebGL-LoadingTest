// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectSpawner.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab = null;

	[SerializeField]
	private int spawnCooldownFrames = 15;

	[SerializeField]
	private int maxInstances = 200;

	private int counter = 0;
	private int instances = 0;
	private Queue<GameObject> spawnedObjects = null;

	void Awake()
	{
		spawnedObjects = new Queue<GameObject>(maxInstances + 5);
	}

	void Update()
	{
		counter++;
		if (counter >= spawnCooldownFrames)
		{
			SpawnObject();
			counter = 0;
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