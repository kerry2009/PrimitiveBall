using UnityEngine;
using System.Collections;

public class CloudsManager : MonoBehaviour {
	public Transform cloudLeft;
	public Transform cloudRight;

	private Cloud[] clouds;
	private int numClouds;

	// Use this for initialization
	void Start () {
		clouds = transform.GetComponentsInChildren<Cloud> ();
		numClouds = clouds.Length;
	}

	public void MoveClouds(float scrollSpeedX, float scrollSpeedY, float ratioX, float ratioY) {
		Cloud c;
		for (int i = 0; i < numClouds; i++) {
			c = clouds[i];
			c.moveXSpeed = -0.5f;
			if (c.transform.position.x < cloudLeft.position.x) {
				c.ResetCloudToPos(cloudRight.position.x + (c.transform.position.x - cloudLeft.position.x));
			} else {
				c.CloudMove(scrollSpeedX * ratioX);
			}
		}

		Vector3 pos = transform.position;
		pos.y += scrollSpeedY * (1f - ratioY);
		transform.position = pos;
	}

}
