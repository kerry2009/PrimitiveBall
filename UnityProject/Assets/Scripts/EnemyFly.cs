using UnityEngine;
using System.Collections;

public class EnemyFly : Enemy {
	override protected void UpdateEnemyPos() {
		Vector3 tranPos = transform.position;
		tranPos.x += moveXSpeed;
		
		if (isDead) {
			moveYSpeed += Global.GRIVATY;
		}

		tranPos.y += moveYSpeed;
		transform.position = tranPos;
	}

}
