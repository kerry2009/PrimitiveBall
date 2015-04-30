using UnityEngine;
using System.Collections;

public class EnemyFloor : Enemy {
	public float reboundX;
	public float reboundY;
	public float reboundUpY;

	override protected void OnMove() {
		Vector3 tranPos = transform.localPosition;
		tranPos.x += moveXSpeed * Time.deltaTime;
		transform.localPosition = tranPos;
	}

	override public void OnHit(Geek geek) {
		float rebound = Global.player.playProperties.EnemyRebound;
		geek.speedX += reboundX * rebound;
		if (geek.speedY <= 0) {
			geek.speedY += reboundUpY * rebound;
		} else {
			geek.speedY += reboundY * rebound;
		}
		OnAddCoins();

		CameraEffects ce = Camera.main.GetComponent<CameraEffects>();
		ce.SetBulletTime (0.1f, 3f);
	}

}
