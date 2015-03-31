using UnityEngine;
using System.Collections;

public class ArenaGameManager : MonoBehaviour {
	public Geek geek;
	public Hero hero;

	public Camera mainCamera;
	public GameObject cameraRestrictCorner;

	public Animator FlashScreen;
	public bool gameOvered;
	public int gameModePhrase;

	public Transform bgBack;
	public Transform bgMiddle;
	public Transform bgFront;
	public Transform floor;
	public Transform bgSky;
	public Transform bgStars;
	public Transform coloudSpawner;
	public Transform enemySpawner;

	public Transform followObject;

	public HitMeter meter;

	public float bgFrontScrollRatio = 1.0f;
	public float bgMidScrollRatio = 0.5f;
	public float bgBackScrollRatio = 0.1f;

	private Vector2 lastCameraPos;

	public EnemySpawner[] EnemyGenSpawners;
	public Transform skyVanishiStart;
	public Transform skyVanishiEnd;
	public bool resposeMouseClick;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	void Start() {
		gameOvered = false;
		gameModePhrase = 0;

		resposeMouseClick = true;
		lastCameraPos = new Vector2 (mainCamera.transform.position.x, mainCamera.transform.position.y);

		HeroItemConfig heroCfg = Global.player.playProperties.hero;
		meter.InitMeter (0.5f, heroCfg.angleMin, heroCfg.angleMax);
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

		Vector3 skyPos = bgSky.transform.position;
		if (skyPos.y > skyVanishiStart.position.y) {
			SpriteRenderer sr = bgSky.GetComponent<SpriteRenderer> ();
			Color skyColor = sr.color;

			if (skyPos.y > skyVanishiEnd.position.y) {
				skyColor.a = 0f;
			} else {
				float diff = (skyPos.y - skyVanishiStart.position.y) / (skyVanishiEnd.position.y - skyVanishiStart.position.y);
				skyColor.a = 1f - diff;
			}

			sr.color = skyColor;
		} else {
			SpriteRenderer sr = bgSky.GetComponent<SpriteRenderer> ();
			Color skyColor = sr.color;
			skyColor.a = 1.0f;
			sr.color = skyColor;
		}
	}

	void LateUpdate() {
		if (followObject) {
			cameraFollower (followObject);
		}
	}

	private void cameraFollower(Transform followee) {
		Vector3 cameraPos = mainCamera.transform.position;
		cameraPos.x = followee.position.x;
		cameraPos.y = followee.position.y;
		
		float  limitBottom = cameraRestrictCorner.transform.position.y;
		if (cameraPos.y < limitBottom) {
			cameraPos.y = limitBottom;
		}
		
		float  limitLeft = cameraRestrictCorner.transform.position.x;
		if (cameraPos.x < limitLeft) {
			cameraPos.x = limitLeft;
		}

		mainCamera.transform.position = cameraPos;

		float offsetX = cameraPos.x - lastCameraPos.x;
		float offsetY = cameraPos.y - lastCameraPos.y;

		lastCameraPos.x = cameraPos.x;
		lastCameraPos.y = cameraPos.y;

		moveBackGround (bgBack, offsetX, 0, offsetX * bgBackScrollRatio, 0);
		moveBackGround (bgMiddle, offsetX, 0, offsetX * bgMidScrollRatio, 0);
		moveBackGround (bgFront, offsetX, 0, offsetX * bgFrontScrollRatio, 0);
		moveBackGround (bgSky, offsetX, offsetY, 0, 0);
		moveBackGround (bgStars, offsetX, offsetY, offsetX, offsetY);
		moveBackGround (coloudSpawner, offsetX, 0, 0, 0);
		moveBackGround (enemySpawner, offsetX, 0, 0, 0);
	}

	private Vector3 transPos = new Vector3 ();
	private Vector2 texturePos = new Vector2 ();
	private void moveBackGround(Transform bgTrans, float offsetX, float offsetY, float textureOffsetX, float textureOffsetY) {
		transPos.x = bgTrans.position.x + offsetX;
		transPos.y = bgTrans.position.y + offsetY;
		transPos.z = bgTrans.position.z;

		if (textureOffsetX != 0f || textureOffsetY != 0f) {
			Material mat = bgTrans.GetComponent<Renderer>().material;
			texturePos.x = mat.mainTextureOffset.x + textureOffsetX;
			texturePos.y = mat.mainTextureOffset.y + textureOffsetY;
			mat.mainTextureOffset = texturePos;
		}

		bgTrans.position = transPos;
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
