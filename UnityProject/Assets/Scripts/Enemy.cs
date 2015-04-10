using UnityEngine;
using System.Collections;

public class Enemy : MovableGameObject {
	private static BloodManager bloodManager = null;
	public const int BLOOD_GEN_MAX = 20;

	private int curbloodNum = 0;
	private Animator animator;

	void Awake() {
		if (bloodManager == null) {
			bloodManager = GameObject.Find ("BloodManager").GetComponent<BloodManager>();
		}
		animator = GetComponent<Animator>();
	}

	public override void OnInit () {
		curbloodNum = 0;

		moveXSpeed = 0;
		moveYSpeed = 0;
		isDead = false;
	}

	public void SetDeadSpeed(float sX, float sY) {
		moveXSpeed = sX;
		moveYSpeed = sY;
	}

	public void OnEnemyDead() {
		animator.Play ("EnemyDie");
		isDead = true;
		curbloodNum = 0;
	}

	protected virtual void UpdateEnemyPos() {
	}

	public void Update() {
		UpdateEnemyPos ();

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
