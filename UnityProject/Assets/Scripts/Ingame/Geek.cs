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
	private float startX;
	private float startFloorY;

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

		startX = transform.position.x;
		startFloorY = floor.position.y;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();

		if (enemy && !enemy.isDead) {
			enemy.OnHit(this);
			enemy.OnAddCoins();

			if (enemy.gameObject.tag == "EnemyFloor") {
				enemy.SetDeadSpeed(0, 0);
			} else if (enemy.gameObject.tag == "EnemyFly") {
				enemy.SetDeadSpeed(0, -0.9f);
			}

			enemy.OnEnemyDead();
		}
	}

	public void SetGravity(float g) {
		gravity = g;
	}

	public void SetFloorFriction(float fX, float fY) {
		floorFrictionX = fX;
		floorFrictionY = fY;
	}

	void Update() {
		if (gameManager.gameOvered) {
			return;
		}

		if (paused) {
			SetGeekRotation (-rotation);
			return;
		}

		float floorY = floor.position.y;

		speedY += gravity;
		Vector3 vect = transform.position;

		// check hit floor
		if (vect.y - circleCollider2d.radius < floorY) {
			vect.y = floorY + circleCollider2d.radius;

			speedX *= floorFrictionX;
			speedY *= -floorFrictionY;

			vect.x += speedX * Time.deltaTime;
			vect.y += speedY * Time.deltaTime;

			// check is dead
			if (speedX < 0.005f) {
				vect.y = floorY + circleCollider2d.radius;
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
			vect.x += speedX * Time.deltaTime;
			vect.y += speedY * Time.deltaTime;
		}

		transform.position = vect;

		if (arrowHitRotation) {
			rotation = -Mathf.Atan2(speedY * 0.5f, speedX) * (180 / Mathf.PI) + 90f;
		} else {
			rotation += reboundRot * Time.deltaTime;
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

	public float Height {
		get {
			return transform.position.y - startFloorY;
		}
	}

	public float Distance {
		get {
			return transform.position.x - startX;
		}
	}

}
