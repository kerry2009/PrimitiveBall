using UnityEngine;
using System.Collections;

public class EnemyFly : Enemy {

	public float reboundY;

	override protected void OnMove() {
		Vector3 tranPos = transform.localPosition;
		tranPos.x += moveXSpeed * Time.deltaTime;
		
		if (isDead) {
			moveYSpeed += Global.GRIVATY;
		}

		tranPos.y += moveYSpeed * Time.deltaTime;
		transform.localPosition = tranPos;
	}

	override public void OnHit(Geek geek) {
		geek.speedY += reboundY * Global.player.playProperties.EnemyRebound;
		OnAddCoins();
	}

}
