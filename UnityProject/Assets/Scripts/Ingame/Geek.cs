﻿using UnityEngine;
using System.Collections;

public class Geek : MonoBehaviour {
	public Transform floor;
	public ArenaGameManager gameManager;

	public float speedX;
	public float speedY;
	public bool paused = false;

	private float reboundRot;
	private bool arrowHitRotation;
	private float rotation;

	private float gravity;
	private float floorFrictionX;
	private float floorFrictionY;

	private Animator animator;
	private CircleCollider2D circleCollider2d;

	void Awake() {
		animator = GetComponent<Animator>();
		circleCollider2d = GetComponent<CircleCollider2D>();
	}

	void Start () {
		gravity = 0;
		rotation = 0;
		paused = false;
		speedX = 0f;
		speedY = 0f;
		arrowHitRotation = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();

		if (enemy && !enemy.isDead) {
			enemy.OnEnemyDead();

			if (enemy.gameObject.tag == "EnemyFloor") {
				OnHitEnemyFloor();
			} else if (enemy.gameObject.tag == "EnemyFly") {
				enemy.SetDeadSpeed(gameManager.geek.speedX * 0.9f, gameManager.geek.speedY * 0.9f);
				OnHitEnemyFly();
			}

		}
	}

	public void OnHitEnemyFloor() {
		float rebound = Global.player.playProperties.EnemyRebound;
		speedX += Global.ENEMY_FLOOR_X * rebound;
		if (speedY <= 0) {
			speedY += Global.ENEMY_FLOOR_ADD_Y * rebound;
		} else {
			speedY += Global.ENEMY_FLOOR_Y * rebound;
		}
	}

	public void OnHitEnemyFly() {
		speedY += Global.ENEMY_FLY_Y * Global.player.playProperties.EnemyRebound;
	}

	public void SetGravity(float g) {
		gravity = g;
	}

	public void SetFloorFriction(float fX, float fY) {
		floorFrictionX = fX;
		floorFrictionY = fY;
	}

	void Update() {
		if (paused) {
			SetGeekRotation (-rotation);
			return;
		}

		speedY += gravity;
		Vector3 vect = transform.position;

		// check hit floor
		if (vect.y - circleCollider2d.radius < floor.position.y) {
			vect.y = floor.position.y + circleCollider2d.radius;

			speedX *= floorFrictionX;
			speedY *= -floorFrictionY;

			vect.x += speedX * 0.1f;
			vect.y += speedY * 0.1f;

			// check is dead
			if (speedX < 0.005f) {
				vect.y = floor.position.y + circleCollider2d.radius;
				rotation = 0;
				if (!gameManager.gameOvered) {
					gameManager.gameOver();
					PlayDeadAnimation();
				}
			} else {
				arrowHitRotation = false;
				// hit floor and rotation
				CallFloorRebound();
				PlayFlyAnimation(Random.Range(2, 5));
			}
		} else {
			vect.x += speedX * 0.1f;
			vect.y += speedY * 0.1f;
		}

		transform.position = vect;

		if (arrowHitRotation) {
			rotation = -Mathf.Atan2(speedY * 0.5f, speedX) * (180 / Mathf.PI) + 90f;
		} else {
			rotation += reboundRot;
			reboundRot *= 0.95f;
		}

		SetGeekRotation (-rotation);
	}

	private static Vector3 vect = new Vector3 ();
	private void SetGeekRotation(float rot) {
		vect.x = 0;
		vect.y = 0;
		vect.z = rot;
		transform.localRotation = Quaternion.Euler(vect);
	}

	public void SetArrowHit(bool isArrowHitRot) {
		arrowHitRotation = isArrowHitRot;
		PlayFlyAnimation (1);
	}

	private void CallFloorRebound() {
		arrowHitRotation = false;
		reboundRot = (speedX * speedX + speedY * speedY) * 100f;
	}

	public void PlayFlyAnimation(int state) {
		animator.Play ("GeekFly" + state);
	}

	public void PlayDeadAnimation() {
		animator.Play ("GeekDead" + Random.Range(1, 3));
	}

	public void PlayIdle() {
		animator.Play ("GeekIdle");
	}

}