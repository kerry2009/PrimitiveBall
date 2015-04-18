using UnityEngine;
using System.Collections;

public class EnemyFly : Enemy {

	public float reboundY;

	override protected void OnMove() {
		Vector3 tranPos = transform.position;
		tranPos.x += moveXSpeed;
		
		if (isDead) {
			moveYSpeed += Global.GRIVATY;
		}

		tranPos.y += moveYSpeed;
		transform.position = tranPos;
	}

	override public void OnHit(Geek geek) {
		geek.speedY += reboundY * Global.player.playProperties.EnemyRebound;
	}

}
