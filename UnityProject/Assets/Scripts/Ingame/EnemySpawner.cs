using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public Geek geek;
	public Enemy[] prefabs; // Array of prefabs.
	public float spawnXDist;

	public Transform leftEdge;
	public Transform rightEdge;
	public Transform container;
	
	protected List<Enemy> spawnedObjects;
	protected List<Enemy> outScreenObjects;

	private float oldSpawnX = 0f;

	void Awake() {
		spawnedObjects = new List<Enemy> ();
		outScreenObjects = new List<Enemy> ();
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
		
		Enemy enemy;
		for (int i = len - 1; i >= 0; i--) {
			enemy = spawnedObjects[i];
			if (enemy.transform.position.x < left || enemy.transform.position.x > right) {
				enemy.gameObject.SetActive (false);
				outScreenObjects.Add(enemy);
				spawnedObjects.RemoveAt(i);
			}
		}

		if (transform.position.x - oldSpawnX > spawnXDist) {
			oldSpawnX = transform.position.x;
			OnSpawnEnemies();
		}
	}

	protected virtual void OnSpawnEnemies() {
	}

	private static Quaternion zeroRotation = new Quaternion();
	protected void SpawnEnemy(float spawnPosX, float spawnPosY) {
		foreach (Enemy prefabEnemy in prefabs) {
			if (Random.value <= prefabEnemy.createRate) {
				Enemy enemy;

				if (outScreenObjects.Count > 0) {
					enemy = outScreenObjects[0];
					outScreenObjects.RemoveAt(0);

					enemy.animator.runtimeAnimatorController = prefabEnemy.animator.runtimeAnimatorController;

					// debug only
					SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
					sr.color = prefabEnemy.GetComponent<SpriteRenderer>().color;

				} else {
					enemy = Instantiate(prefabEnemy, transform.position, zeroRotation)  as Enemy;
				}

				Vector3 p = enemy.transform.position;
				p.x = spawnPosX;
				p.y = spawnPosY;
				enemy.transform.position = p;

				// init enemy
				enemy.OnInit (geek.speedX * Global.Enemy_SPEED_RATE_MIN, geek.speedX * Global.Enemy_SPEED_RATE_MAX);
				enemy.transform.SetParent (container, false);
				spawnedObjects.Add(enemy);
			}
		}
	}

}