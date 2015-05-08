using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public ArenaGameManager gameManager;
	public BackgroundManager backgroundManager;
	public Geek geek;
	public Transform hitDivLine;
	public Transform heroRunFloor;
	public float smoothTime;
	public ParticleSystem bloodStrikeEffect;

	private int currentState;
	private int targetState;
	private Animator animator;
	private float floorY;

	private const int STATE_IDLE = 0;
	private const int STATE_RUN = 1;
	private const int STATE_AIRHIT = 2;
	private const int STATE_GROUNDHIT = 3;
	private Vector3 velocity = Vector3.zero;

	void Awake() {
		animator = GetComponent<Animator> ();
		floorY = heroRunFloor.position.y;

		currentState = STATE_IDLE;
		targetState = STATE_IDLE;
	}

	void LateUpdate () {
		Vector3 heroPos = transform.position;

		AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo (0);
		if (asi.IsName("HeroRun")) {
			if (currentState != STATE_RUN) {
				heroPos.x = geek.transform.position.x;
				heroPos.y = floorY;
				currentState = STATE_RUN;
				transform.position = heroPos;
			} else {
				MoveTowardGeek();
			}
		}

		if (currentState != targetState) {
			if (targetState == STATE_GROUNDHIT) {
				currentState = STATE_GROUNDHIT;
				targetState = STATE_RUN;
				transform.position = heroPos;
			} else if (targetState == STATE_AIRHIT) {
				heroPos.x = geek.transform.position.x;
				heroPos.y = geek.transform.position.y;
				currentState = STATE_AIRHIT;
				targetState = STATE_RUN;
				transform.position = heroPos;
			}
		}
	}

	public void MoveTowardGeek() {
		Vector3 targetPosition = geek.transform.position;
		targetPosition.y = floorY;
		targetPosition.z = transform.position.z;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
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
	}

	private void SmashGround() {
		float pow = Global.player.playProperties.WeaponPower * Global.player.playProperties.SmashPower;

		geek.speedX += pow * Global.SMASH_GROUND_X + Global.SMASH_GROUND_X;

		if (geek.speedY >= 0) {
			geek.speedY += pow * Global.SMASH_GROUND_Y + Global.SMASH_GROUND_Y;
		} else {
			geek.speedY = (pow * Global.SMASH_GROUND_Y + Global.SMASH_GROUND_Y) - geek.speedY;
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
		backgroundManager.followObject = transform;
		geek.paused = true;
	}

	public void resumeGeekMove() {
		gameManager.showFlashScreen ();
		geek.paused = false;
		geek.OnHeroHit ();
	}

	public void cameraOnHero() {
		backgroundManager.followObject = transform;
	}

	public void cameraOnGeek() {
		backgroundManager.followObject = geek.transform;
		backgroundManager.CameraSmoothToGeek ();
	}

	public void PlayBloodStrikeEffect() {
		bloodStrikeEffect.Play (true);
	}

}
