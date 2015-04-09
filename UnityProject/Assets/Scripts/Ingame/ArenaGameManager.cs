using UnityEngine;
using System.Collections;

public class ArenaGameManager : MonoBehaviour {
	public Geek geek;
	public Hero hero;

	public Animator FlashScreen;

	public Transform followObject;
	public HitMeter meter;

	public EnemySpawner[] EnemyGenSpawners;

	private int gameModePhrase;
	private bool resposeMouseClick;
	public bool gameOvered;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		gameOvered = false;
		gameModePhrase = 0;

		resposeMouseClick = true;

		HeroItemConfig heroCfg = Global.player.playProperties.hero;
		meter.InitMeter (0.5f, heroCfg.angleMin, heroCfg.angleMax, heroCfg.criticalHitAngleMin, heroCfg.criticalHitAngleMax);
		meter.RunAngleCursor ();
		meter.easeIn ();

		hero.PlayHeroStand ();
		geek.PlayIdle ();

		InitEnemySpawners ();
	}

	private void InitEnemySpawners() {
		float spawnDist = Global.player.playProperties.EnemyAppear;
		foreach (EnemySpawner es in EnemyGenSpawners) {
			es.spawnXDist = spawnDist;
		}
	}

	// Update is called once per frame
	void Update () {
		if (resposeMouseClick) {
			if (Input.GetMouseButtonUp(0)) {
				switch (gameModePhrase) {
				case 0 :
					selectedPower();
					break;
				case 1:
					selectAngleAndHit();
					break;
				default:
					onHit();
					break;
				}
			}
		}
	}

	private void selectedPower() {
		gameModePhrase = 1;

		meter.StopAngleCursor ();
		meter.RunPowerCursor ();

		hero.PlayHeroReady ();
	}
	
	private void selectAngleAndHit() {
		gameModePhrase = 2;
		
		meter.StopPowerCursor ();
		meter.easeOut ();

		hitGeekFirst ();
	}

	private void hitGeekFirst() {
		hero.PlayHeroStartHit ();

		FlashScreen.gameObject.SetActive (true);
		showFlashScreen ();

		PlayProperties pp = Global.player.playProperties;

		float hitAngle = meter.GetAnglePercentage ();
		float firstHitPower = pp.GetFirstHitPower (hitAngle) * meter.GetPowerPercentage ();
		float firstHitAngle = hitAngle * (Mathf.PI / 180);

		geek.SetFloorFriction (pp.FloorFriction, pp.FloorFriction);
		geek.SetGravity(Global.GRIVATY);
		geek.speedX = Mathf.Cos (firstHitAngle) * firstHitPower;
		geek.speedY = Mathf.Sin (firstHitAngle) * firstHitPower;

		geek.SetArrowHit (true);
	}

	public void showFlashScreen() {
		FlashScreen.Play ("FlashScreen", 0, 0f);
	}

	private void onHit() {
		hero.hit ();
	}

	public void gameOver() {
		if (gameOvered == false) {
			Debug.Log("Game over!");
			gameOvered = true;
		}
	}

	public void LoadNaviMapScene () {
		Application.LoadLevel ("NaviMap");
	}

}
