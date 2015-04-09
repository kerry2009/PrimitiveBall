using UnityEngine;
using System.Collections;

public class InfiniteScrollBackground : MonoBehaviour {
	public Transform imgs;
	public Transform head;

	private float resetLeft;
	private float curLeft;
	private float imageSizeX;
	private float backGroundBottom;

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = head.GetComponent<SpriteRenderer> ();

		imageSizeX = sr.bounds.extents.x * 2;
		resetLeft = head.localPosition.x;
		curLeft = resetLeft;

		backGroundBottom = transform.position.y;
	}

	public void MoveBackground (float scrollSpeedX, float scrollSpeedY, float ratioX, float ratioY) {
		Vector3 pos = transform.position;
		pos.x += scrollSpeedX;
		pos.y += scrollSpeedY * ratioY;
		if (pos.y < backGroundBottom) {
			pos.y = backGroundBottom;
		}
		transform.position = pos;

		Vector3 imagesVect = imgs.transform.localPosition;
		curLeft -= scrollSpeedX * ratioX;

		if (curLeft < resetLeft - imageSizeX) {
			curLeft = resetLeft - (resetLeft - imageSizeX - curLeft) % imageSizeX;
		}

		imagesVect.x = curLeft;
		imgs.transform.localPosition = imagesVect;
	}


}
