using UnityEngine;
using System.Collections;

public class EnemySpawner : RangeSpawner {

	protected override void OnInitObject(MovableGameObject movable) {
		movable.moveXSpeed = Random.Range (0.1f, 0.5f);
		movable.moveYSpeed = 0;
	}

}
