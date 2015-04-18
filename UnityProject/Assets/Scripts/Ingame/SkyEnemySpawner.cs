using UnityEngine;
using System.Collections;

public class SkyEnemySpawner : EnemySpawner {
	public Transform[] points;

	override protected void OnSpawnEnemies() {
		int len = points.Length;

		Vector3 startPos;
		Vector3 endPos;
		for (int i = 0; i < len; i++) {
			if (i + 1 < len) {
				startPos = points[i].position;
				endPos = points[i + 1].position;
				SpawnEnemy (startPos.x, Random.Range(startPos.y, endPos.y));
			} else {
				break;
			}
		}
	}

}
