using UnityEngine;
using System.Collections;

public class Geek : MonoBehaviour {
	public Transform floor;
	public ArenaGameManager gameManager;

	private Vector3 vect = new Vector3();

	public float rotation;
	public float speedX;
	public float speedY;
	public float friction;

	private Animator animator;
	private float reboundRot;
	private bool arrowHitRotation;

	public bool paused = false;

	CircleCollider2D circleCollider2d;

	void Start () {
		rotation = 0;
		paused = false;
		speedX = 0f;
		speedY = 0f;
		friction = 0.6f;
		arrowHitRotation = false;

		circleCollider2d = GetComponent<CircleCollider2D>();
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();

		if (enemy && !enemy.isDead) {
			enemy.OnEnemyDead();

			if (enemy.gameObject.tag == "EnemyFloor") {
				enemy.moveYSpeed = -0.1f;
				enemy.moveXSpeed = gameManager.geek.speedX * 0.8f;
			}

			// lift up geek
			speedY += 0.4f;
		}
	}

	void Update() {
		if (paused) {
			SetGeekRotation (-rotation);
			return;
		}

		vect.x = transform.position.x;
		vect.y = transform.position.y;
		vect.z = transform.position.z;
		
		speedY += gameManager.gravity;

		// check hit floor
		if (vect.y - circleCollider2d.radius < floor.position.y) {
			vect.y = floor.position.y + circleCollider2d.radius;

			speedX *= friction * 0.5f;
			speedY *= -friction;
			// Debug.Log("spX:" + speedX + ", spY:" + speedY + ", friction:" + friction);

			vect.x += speedX;
			vect.y += speedY;

			// check is dead
			if (speedX < 0.005f) {
				vect.y = floor.position.y + circleCollider2d.radius;
				rotation = 0;
				if (!gameManager.gameOvered) {
					gameManager.gameOver();
					playDeadAnimation();
				}
			} else {
				arrowHitRotation = false;
				// hit floor and rotation
				CallFloorRebound();
				playFlyAnimation(Random.Range(2, 5));
			}
		} else {
			vect.x += speedX;
			vect.y += speedY;
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

	private void SetGeekRotation(float rot) {
		vect.x = 0;
		vect.y = 0;
		vect.z = rot;
		transform.localRotation = Quaternion.Euler(vect);
	}

	public void SetArrowHit(bool isArrowHitRot) {
		arrowHitRotation = isArrowHitRot;
		playFlyAnimation (1);
	}

	private void CallFloorRebound() {
		arrowHitRotation = false;
		reboundRot = (speedX * speedX + speedY * speedY) * 100f;
	}

	public void playFlyAnimation(int state) {
		animator.Play ("GeekFly" + state);
	}

	public void playDeadAnimation() {
		animator.Play ("GeekDead" + Random.Range(1, 3));
	}

}
