using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public ArenaGameManager gameManager;
	public Animator animator;
	public Geek geek;
	public Transform hitDivLine;
	public Transform heroRunFloor;

	private int currentState;
	private int targetState;
	private float speedX;

	private const int STATE_IDLE = 0;
	private const int STATE_RUN = 1;
	private const int STATE_AIRHIT = 2;
	private const int STATE_GROUNDHIT = 3;

	// Use this for initialization
	void Start () {
		speedX = 0;
		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
	}

	void Update () {
		Vector3 heroPos = transform.position;

		AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo (0);
		if (asi.IsName("HeroRun")) {
			CalcHeroRunPos(ref heroPos);
			currentState = STATE_RUN;
		}

		if (currentState != targetState && targetState == STATE_GROUNDHIT) {
			CalcHeroRunPos(ref heroPos);
			currentState = STATE_GROUNDHIT;
			targetState = STATE_RUN;
		}

		if (currentState != targetState && targetState == STATE_AIRHIT) {
			heroPos.x = geek.transform.position.x;
			heroPos.y = geek.transform.position.y;
			currentState = STATE_AIRHIT;
			targetState = STATE_RUN;
			speedX = 0;
		}

		heroPos.x += speedX;
		transform.position = heroPos;
	}

	public void CalcHeroRunPos(ref Vector3 heroPos) {
		float dist = geek.transform.position.x - transform.position.x;

		if (dist >= 0) {
			if (dist >= 4.0f) {
				speedX = dist * 0.5f;
			} else {
				speedX = 0.2f;
			}

			if (speedX > dist) {
				speedX = dist;
			}
		} else {
			speedX = 0;
		}

		heroPos.y = heroRunFloor.position.y;
	}

	public void hit() {
		// air hit
		if (geek.transform.position.y > hitDivLine.position.y) {
			SmashAir();
			playAirHit();
		} else { // ground hit
			SmashGround();
			playGroundHit();
		}

		geek.SetArrowHit (true);
	}

	private void SmashGround() {
		float pow = Global.player.playProperties.WeaponPower * Global.player.playProperties.SmashPower;

		geek.speedX += pow * Global.SMASH_GROUND_X + Global.SMASH_GROUND_X;

		if (geek.speedY >= 0) {
			geek.speedY += pow * Global.SMASH_GROUND_Y + Global.SMASH_GROUND_Y;
		} else {
			geek.speedY = (pow * Global.SMASH_GROUND_Y + Global.SMASH_GROUND_Y) + geek.speedY * -1.0f;
		}
	}

	private void SmashAir() {
		geek.speedX += Global.player.playProperties.WeaponPower * Global.player.playProperties.SmashPower * Global.SMASH_AIR_X + Global.SMASH_AIR_X;
		geek.speedY = Global.SMASH_AIR_Y;
	}

	public void PlayHeroStand() {
		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
		animator.Play ("HeroStand");
	}

	public void PlayHeroReady() {
		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
		animator.Play ("HeroReady");
	}

	public void PlayHeroStartHit() {
		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
		animator.Play ("HeroStartHit");
	}

	private void playAirHit() {
		targetState = STATE_AIRHIT;
		animator.Play ("HeroAirHit");
	}

	private void playGroundHit() {
		gameManager.showFlashScreen ();
		targetState = STATE_GROUNDHIT;
		animator.Play ("HeroGroundHit");
	}

	public void pauseGeekMove() {
		gameManager.followObject = null;
		geek.paused = true;
	}

	public void resumeGeekMove() {
		gameManager.showFlashScreen ();
		geek.paused = false;
	}

	public void cameraOnHero() {
		gameManager.followObject = transform;
	}

	public void cameraOnGeek() {
		gameManager.followObject = geek.transform;
	}

}
