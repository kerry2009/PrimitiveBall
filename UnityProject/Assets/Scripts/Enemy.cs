using UnityEngine;
using System.Collections;

public class Enemy : MovableGameObject {
	public const int BLOOD_GEN_MAX = 20;
	public float gainCoins;
	public float createRate;

	private static BloodManager bloodManager = null;
	private int curbloodNum = 0;
	private Animator _animator;

	void Awake() {
		if (bloodManager == null) {
			bloodManager = GameObject.Find ("BloodManager").GetComponent<BloodManager>();
		}
	}

	public virtual void OnHit(Geek geek) {
	}

	public void OnAddCoins() {
		Global.player.coins += (int)(gainCoins * Global.player.playProperties.AddXpCoins);
	}

	public void OnInit (float speedXMin, float speedXMax) {
		curbloodNum = 0;
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
			curbloodNum = 0;
		}
	}

	protected virtual void OnMove() {
	}

	public void Update() {
		OnMove ();

		if (isDead && bloodManager) {

			if (curbloodNum < BLOOD_GEN_MAX) {

				bloodManager.AddBlood(transform.position);
				
				curbloodNum++;

			} else {
				gameObject.SetActive (false);
			}
		}
	}

}
