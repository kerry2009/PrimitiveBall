using UnityEngine;
using System.Collections;

public class EnemyFloor : Enemy {

	override protected void UpdateEnemyPos() {
		Vector3 tranPos = transform.position;
		tranPos.x += moveXSpeed;
		tranPos.y += moveYSpeed;
		transform.position = tranPos;
	}

}
