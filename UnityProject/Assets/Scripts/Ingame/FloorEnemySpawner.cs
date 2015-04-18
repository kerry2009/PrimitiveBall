using UnityEngine;
using System.Collections;

public class FloorEnemySpawner : EnemySpawner {

	override protected void OnSpawnEnemies() {
		SpawnEnemy (transform.position.x, transform.position.y);
	}

}
