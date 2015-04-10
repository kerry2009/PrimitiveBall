﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public MovableGameObject[] profabs; // Array of prefabs.

	public float spawnXDist;
	
	public Transform rangeTopPoint;
	public Transform leftEdge;
	public Transform rightEdge;
	public Transform container;
	
	protected List<MovableGameObject> spawnedObjects;
	protected List<MovableGameObject> outScreenObjects;

	private float oldSpawnX = 0f;

	public EnemySpawner() {
		spawnedObjects = new List<MovableGameObject> ();
		outScreenObjects = new List<MovableGameObject> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int len = spawnedObjects.Count;
		float left, right;
		
		if (leftEdge != null) {
			left = leftEdge.position.x;
		} else {
			left = int.MinValue;
		}
		
		if (rightEdge != null) {
			right = rightEdge.position.x;
		} else {
			right = int.MaxValue;
		}
		
		MovableGameObject mobj;
		for (int i = len - 1; i >= 0; i--) {
			mobj = spawnedObjects[i];
			if (mobj.transform.position.x < left || mobj.transform.position.x > right) {
				mobj.gameObject.SetActive (false);
				outScreenObjects.Add(mobj);
				spawnedObjects.RemoveAt(i);
			}
		}
		
		if (transform.position.x - oldSpawnX > spawnXDist) {
			oldSpawnX = transform.position.x;
			SpawnGameObject();
		}
	}
	
	protected void SpawnGameObject() {
		Spawn(OnSpawnObject(), rangeTopPoint);
	}
	
	private static Quaternion zeroRotation = new Quaternion();
	protected virtual MovableGameObject OnSpawnObject() {
		MovableGameObject mobj;
		
		if (outScreenObjects.Count > 0) {
			mobj = outScreenObjects[0];
			outScreenObjects.RemoveAt(0);
		} else {
			// Instantiate a random enemy.
			int randIndex = Random.Range(0, profabs.Length);
			mobj = Instantiate(profabs[randIndex], transform.position, zeroRotation)  as MovableGameObject;
		}
		
		mobj.gameObject.SetActive (true);
		return mobj;
	}

	protected void Spawn(MovableGameObject mobj, Transform endTopPoint) {
		// set Spawn point (random y)
		Vector3 p = transform.position;
		p.y = Random.Range(transform.position.y, endTopPoint.position.y);
		transform.position = p;
		mobj.transform.SetParent (container, false);

		OnInitObject (mobj);
		mobj.isDead = false;
		spawnedObjects.Add(mobj);
	}
	
	protected void OnInitObject(MovableGameObject movable) {
		movable.moveXSpeed = Random.Range (0.1f, 0.5f);
		movable.moveYSpeed = 0;
	}

}