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

	public bool paused = false;

	CircleCollider2D circleCollider2d;

	void Start () {
		paused = false;
		speedX = 0f;
		speedY = 0f;
		friction = 0.6f;

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
				CallRebound();
			} else if (enemy.gameObject.tag == "EnemyFly") {
				CallRebound();
			}

			// lift up geek
			speedY += 0.4f;
		}
	}

	void Update() {
		if (paused) {
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
				CallRebound();
				playFlyAnimation(Random.Range(2, 5));
			}
		} else {
			vect.x += speedX;
			vect.y += speedY;
		}

		transform.position = vect;

		GeekRotation ();

		vect.x = 0;
		vect.y = 0;
		vect.z = -rotation;
		transform.localRotation = Quaternion.Euler(vect);
	}

	private void GeekRotation() {
		rotation += reboundRot;
		reboundRot *= 0.95f;
	}

	private void CallRebound() {
		reboundRot = (speedX * speedX + speedY * speedY) * 100f;
	}

	public void playFlyAnimation(int state) {
		animator.Play ("GeekFly" + state);
	}

	public void playDeadAnimation() {
		animator.Play ("GeekDead" + Random.Range(1, 3));
	}

}
