using UnityEngine;
using System.Collections;

public class InfiniteScrollBackground : MonoBehaviour {
	private Transform imgs;
	private Transform head;
	private float imageSizeX;

	private static Vector3 screenLeft = Vector3.zero;

	// Use this for initialization
	void Start () {
		imgs = gameObject.transform.GetChild (0).transform;
		head = imgs.gameObject.transform.GetChild(0).transform;

		imageSizeX = head.GetComponent<SpriteRenderer>().bounds.extents.x;
	}

	public void MoveBackground (float scrollSpeedX, float scrollSpeedY, float ratioX, float ratioY) {
		Vector3 pos = transform.position;
		pos.x += scrollSpeedX;
		pos.y += scrollSpeedY * (1f - ratioY);
		transform.position = pos;

		Vector3 localPos = imgs.localPosition;
		Vector3 outz = Camera.main.ViewportToWorldPoint (screenLeft);
		float headEndX = head.transform.position.x + imageSizeX;

		if (headEndX < outz.x) {
			localPos.x += imageSizeX * 2 - ((outz.x - headEndX) % (imageSizeX * 2));
		}
		localPos.x -= (scrollSpeedX * ratioX) % (imageSizeX * 2);

		imgs.localPosition = localPos;
	}

}