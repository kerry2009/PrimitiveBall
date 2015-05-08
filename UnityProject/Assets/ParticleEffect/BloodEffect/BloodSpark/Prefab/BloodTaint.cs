using UnityEngine;
using System.Collections;

public class BloodTaint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("OnTriggerEnter2D");
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("OnTriggerEnter");
	}

	void OnParticleCollision(GameObject other) {
		Debug.Log ("OnParticleCollision");
	}

}
