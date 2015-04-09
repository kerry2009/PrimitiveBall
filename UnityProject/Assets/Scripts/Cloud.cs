using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public float moveXSpeed = 0f;
	private Vector3 localPos;

	void Start() {
		localPos = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
	}

	public void CloudMove() {
		moveXSpeed = -0.05f;
		localPos.x += moveXSpeed;
		transform.localPosition = localPos;
	}

	public void ResetCloudToPos(float resetPosX) {
		localPos.x = resetPosX;
		transform.localPosition = localPos;
	}

}
