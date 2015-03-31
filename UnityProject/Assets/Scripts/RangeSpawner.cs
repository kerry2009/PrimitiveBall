using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeSpawner : MonoBehaviour {
	public MovableGameObject[] profabs;		// Array of cloud prefabs.

	private float oldSpawnX = 0f;
	public float spawnXDist;

	public Transform rangeTopPoint;
	public Transform leftEdge;
	public Transform rightEdge;
	public Transform gameObjectContainer;

	protected List<MovableGameObject> spawnedObjects;
	protected List<MovableGameObject> outScreenObjects;

	public RangeSpawner() {
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
				//movable.moveXSpeed = 0f;
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
			mobj = Instantiate(profabs[randIndex], p, zeroRotation)  as MovableGameObject;
		}

		mobj.gameObject.SetActive (true);
		return mobj;
	}

	private static Vector3 p = new Vector3 ();
	protected void Spawn(MovableGameObject mobj, Transform endTopPoint) {
		p.x = transform.position.x;
		p.y = Random.Range(transform.position.y, endTopPoint.position.y);
		p.z = transform.position.z;
		OnSetParent (mobj, p);
		OnInitObject (mobj);
		mobj.isDead = false;
		spawnedObjects.Add(mobj);
	}

	protected virtual void OnInitObject(MovableGameObject movable) {
	}

	protected virtual void OnSetParent(MovableGameObject movable, Vector3 mp) {
		mp.z = gameObjectContainer.position.z;

		movable.transform.SetParent (gameObjectContainer, false);

		Transform cTrans = movable.transform;
		cTrans.position = mp;
	}

}
