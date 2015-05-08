using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public ArenaGameManager gameManager;
	public Geek geek;
	public Camera mainCamera;
	public GameObject cameraRestrictCorner;
	public Transform followObject;

	public InfiniteScrollBackground bgBack;
	public InfiniteScrollBackground bgMiddle;
	public InfiniteScrollBackground bgFront;
	public Transform floor;

	public Transform bgSky;
	public Transform bgStars;
	public CloudsManager cloudsFront;
	public CloudsManager cloudsBack;
	public Transform enemyGenerator;

	public Transform skyVanishiStart;
	public Transform skyVanishiEnd;
	public SpriteRenderer skyRenderer;

	private float smoothTime;
	private Vector2 lastGeekPos;
	private Material starsMaterial;
	private Vector3 cameraVelocity;
	private Vector2 starBGOffsetVect2;

	void Start() {
		smoothTime = 0;
		cameraVelocity = Vector3.zero;
		starBGOffsetVect2 = Vector2.zero;
		followObject = geek.transform;
		lastGeekPos = new Vector2 (geek.transform.position.x, geek.transform.position.y);
		starsMaterial = bgStars.GetComponent<Renderer>().sharedMaterials[0];
	}

	void LateUpdate() {
		UpdateSkyColor ();
		CameraFollow ();
	}

	public void CameraSmoothToGeek() {
		iTween.Stop(gameObject);
		iTween.ValueTo(gameObject, iTween.Hash("from", 0.1f, "to", 0f, "time", 1, "onupdate", "UpdateCameraSmooth"));
		iTween.ValueTo(gameObject, iTween.Hash("from", mainCamera.orthographicSize, "to", 8.0f, "time", 2, "onupdate", "OnZooCamera", "oncomplete", "StartZoominCamera"));
	}

	private void UpdateCameraSmooth(float _smoothTime) {
		smoothTime = _smoothTime;
	}

	private void OnZooCamera(float _cameraSize) {
		mainCamera.orthographicSize = _cameraSize;
	}

	private void StartZoominCamera() {
		iTween.ValueTo(gameObject, iTween.Hash("from", mainCamera.orthographicSize, "to", 5.0f, "time", 2, "delay", 1.5f, "onupdate", "OnZooCamera"));
	}

	private void UpdateSkyColor() {
		// Update sky color
		Vector3 geekPos = geek.transform.position;
		if (geekPos.y > skyVanishiStart.position.y) {
			Color skyColor = skyRenderer.color;
			
			if (geekPos.y > skyVanishiEnd.position.y) {
				skyColor.a = 0f;
			} else {
				float diff = (geekPos.y - skyVanishiStart.position.y) / (skyVanishiEnd.position.y - skyVanishiStart.position.y);
				skyColor.a = 1f - diff;
			}
			
			skyRenderer.color = skyColor;
		} else {
			SpriteRenderer sr = bgSky.GetComponent<SpriteRenderer> ();
			Color skyColor = sr.color;
			skyColor.a = 1.0f;
			sr.color = skyColor;
		}
	}

	private void CameraFollow() {
		if (followObject) {
			Vector3 targetPosition = followObject.position;
			targetPosition.z = mainCamera.transform.position.z;
			mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref cameraVelocity, smoothTime);

			float camX = mainCamera.transform.position.x;
			float camY = mainCamera.transform.position.y;

			float offsetX = camX - lastGeekPos.x;
			float offsetY = camY - lastGeekPos.y;
			
			lastGeekPos.x = camX;
			lastGeekPos.y = camY;

//			GroupMoveTo (bgStars, camX, camY);
//			ScrollStarsBG (offsetX * Global.bgStarsScrollX, offsetY * Global.bgStarsScrollY);
//
//			GroupMoveTo (bgSky, camX, bgSky.position.y);
//			GroupMoveTo (floor, camX, bgSky.position.y);
//			GroupMoveTo (enemyGenerator, camX, enemyGenerator.position.y);

			GroupMoveTo (bgStars, offsetX, offsetY);
			ScrollStarsBG (offsetX * Global.bgStarsScrollX, offsetY * Global.bgStarsScrollY);

			GroupMoveTo (bgSky, offsetX, 0);
			GroupMoveTo (floor, offsetX, 0);
			GroupMoveTo (enemyGenerator, offsetX, 0);

			bgFront.MoveBackground (offsetX, offsetY, Global.bgFrontScrollX, Global.bgFrontScrollY);
			bgMiddle.MoveBackground (offsetX, offsetY, Global.bgMidScrollX, Global.bgMidScrollY);
			bgBack.MoveBackground (offsetX, offsetY, Global.bgBackScrollX, Global.bgBackScrollY);

			cloudsFront.MoveClouds (offsetX, offsetY, Global.bgMidScrollX, Global.bgMidScrollY);
			cloudsBack.MoveClouds (offsetX, offsetY, Global.bgBackScrollX, Global.bgBackScrollY);
		}
	}

	private void GroupMoveTo(Transform bgTrans, float posX, float posY) {
		Vector3 transPos = bgTrans.position;
		transPos.x += posX;
		transPos.y += posY;
//		transPos.x = posX;
//		transPos.y = posY;
		bgTrans.position = transPos;
	}

	private void ScrollStarsBG(float offsetX, float offsetY) {
		starBGOffsetVect2.x = Mathf.Repeat(starBGOffsetVect2.x + offsetX, 1.0f);
		starBGOffsetVect2.y = Mathf.Repeat(starBGOffsetVect2.y + offsetY, 1.0f);
		starsMaterial.SetTextureOffset ("_MainTex", starBGOffsetVect2);
	}

	void OnDisable () {
		starsMaterial.SetTextureOffset ("_MainTex", Vector2.zero);
	}

}
