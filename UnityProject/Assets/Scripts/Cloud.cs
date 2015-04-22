using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public float moveXSpeed;
	private Vector3 relativePos;

	void Start() {
		relativePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	public void CloudMove(float scrollSpeedX) {
		relativePos.x += moveXSpeed * Time.deltaTime + scrollSpeedX;
		transform.position = relativePos;
	}

	public void ResetCloudToPos(float resetPosX) {
		relativePos.x = resetPosX;
		transform.position = relativePos;
	}

}
