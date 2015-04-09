using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public ArenaGameManager gameManager;
	public Camera mainCamera;
	public GameObject cameraRestrictCorner;

	public InfiniteScrollBackground bgBack;
	public InfiniteScrollBackground bgMiddle;
	public InfiniteScrollBackground bgFront;

	public Transform bgSky;
	public Transform bgStars;
	public Transform clouds;
	public Transform enemyGenerator;

	public Transform skyVanishiStart;
	public Transform skyVanishiEnd;

	private Vector2 lastCameraPos;
	private float limitLeft;
	private float limitBottom;
	private Material starsMaterial;

	void Start() {
		limitLeft = cameraRestrictCorner.transform.position.x;
		limitBottom = cameraRestrictCorner.transform.position.y;
		lastCameraPos = new Vector2 (mainCamera.transform.position.x, mainCamera.transform.position.y);
		starsMaterial = bgStars.GetComponent<Renderer>().sharedMaterials[0];
	}

	void Update() {
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
		if (gameManager.followObject) {
			cameraFollower (gameManager.followObject);
		}
	}
	
	private void cameraFollower(Transform followee) {
		Vector3 cameraPos = mainCamera.transform.position;
		cameraPos.x = followee.position.x;
		cameraPos.y = followee.position.y;

		if (cameraPos.x < limitLeft) {
			cameraPos.x = limitLeft;
		}

		if (cameraPos.y < limitBottom) {
			cameraPos.y = limitBottom;
		}

		mainCamera.transform.position = cameraPos;
		
		float offsetX = cameraPos.x - lastCameraPos.x;
		float offsetY = cameraPos.y - lastCameraPos.y;

		lastCameraPos.x = cameraPos.x;
		lastCameraPos.y = cameraPos.y;

		bgFront.MoveBackground (offsetX, offsetY, Global.bgFrontScrollX, Global.bgFrontScrollY);
		bgMiddle.MoveBackground (offsetX, offsetY, Global.bgMidScrollX, Global.bgMidScrollY);
		bgBack.MoveBackground (offsetX, offsetY, Global.bgBackScrollX, Global.bgBackScrollY);

		GroupMove (bgStars, offsetX, offsetY);
		GroupMove (bgSky, offsetX, offsetY);
		GroupMove (clouds, offsetX, 0);
		GroupMove (enemyGenerator, offsetX, 0);

		ScrollStarsBG (offsetX * Global.bgStarsScrollX, offsetY * Global.bgStarsScrollY);
	}

	private void GroupMove(Transform bgTrans, float offsetX, float offsetY) {
		Vector3 transPos = bgTrans.position;
		transPos.x += offsetX;
		transPos.y += offsetY;
		bgTrans.position = transPos;
	}

	private static Vector2 offsetVect2 = new Vector2 ();
	private void ScrollStarsBG(float offsetX, float offsetY) {
		offsetVect2.x = Mathf.Repeat(offsetVect2.x + offsetX, 1.0f);
		offsetVect2.y = Mathf.Repeat(offsetVect2.y + offsetY, 1.0f);
		starsMaterial.SetTextureOffset ("_MainTex", offsetVect2);
	}

	void OnDisable () {
		offsetVect2.x = 0;
		offsetVect2.y = 0;
		starsMaterial.SetTextureOffset ("_MainTex", offsetVect2);
	}

}
