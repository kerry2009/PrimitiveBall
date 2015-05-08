using UnityEngine;
using System.Collections;

public class Enemy : MovableGameObject {
	public float gainCoins;
	public float createRate;

	private Animator _animator;

	void Awake() {
	}

	public virtual void OnHit(Geek geek) {
	}

	public void OnAddCoins() {
		Global.player.coins += (int)(gainCoins * Global.player.playProperties.AddXpCoins);
	}

	public void OnInit (float speedXMin, float speedXMax) {
		moveXSpeed = 0;
		moveYSpeed = 0;
		isDead = false;

		moveXSpeed = Random.Range (speedXMin, speedXMax);
		gameObject.SetActive (true);

		animator.Play ("EnemyRun");
	}

	public void SetDeadSpeed(float sX, float sY) {
		moveXSpeed = sX;
		moveYSpeed = sY;
	}

	public Animator animator {
		get {
			if (_animator == null) {
				_animator = GetComponent<Animator>();
			}
			return _animator;
		}
		set {
			_animator = value;
		}
	}

	public void OnEnemyDead() {
		if (!isDead) {
			animator.Play ("EnemyDie");
			isDead = true;
		}
	}

	protected virtual void OnMove() {
	}

	public void Update() {
		OnMove ();
	}

}
