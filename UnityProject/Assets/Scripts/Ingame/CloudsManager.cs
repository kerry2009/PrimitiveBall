using UnityEngine;
using System.Collections;

public class CloudsManager : MonoBehaviour {
	public Transform cloudLeft;
	public Transform cloudRight;

	private Cloud[] clouds;
	private int numClouds;

	private float leftEdge;
	private float rightEdge;

	// Use this for initialization
	void Start () {
		leftEdge = cloudLeft.localPosition.x;
		rightEdge = cloudRight.localPosition.x;

		clouds = transform.GetComponentsInChildren<Cloud> ();
		numClouds = clouds.Length;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Cloud c;
		for (int i = 0; i < numClouds; i++) {
			c = clouds[i];
			if (c.transform.localPosition.x < leftEdge) {
				c.ResetCloudToPos(rightEdge + (c.transform.localPosition.x - leftEdge));
			} else {
				c.CloudMove();
			}
		}
	}

}
