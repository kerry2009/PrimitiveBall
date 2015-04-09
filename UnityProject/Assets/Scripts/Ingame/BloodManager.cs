using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodManager : MonoBehaviour {
	public GameObject bloodProfab;
	public Transform boundLeft;
	public Transform container;

	private List<GameObject> spawnedBloods;
	private List<GameObject> outScreenBloods;

	public BloodManager() {
		spawnedBloods = new List<GameObject> ();
		outScreenBloods = new List<GameObject> ();
	}

	void FixedUpdate() {
		int len = spawnedBloods.Count;
		
		GameObject blood;
		for (int i = len - 1; i >= 0; i--) {
			blood = spawnedBloods[i];
			if (blood.transform.position.x < boundLeft.position.x) {
				outScreenBloods.Add(blood);
				spawnedBloods.RemoveAt(i);
			}
		}
	}

	public void AddBlood(Vector3 bloodPos) {
		// create blood
		GameObject blood;
		if (outScreenBloods.Count > 0) {
			blood = outScreenBloods[0];
			outScreenBloods.RemoveAt(0);
		} else {
			blood = Instantiate(bloodProfab, transform.position, transform.localRotation)  as GameObject;
		}

		spawnedBloods.Add(blood);

		// set blood position
		blood.transform.position = bloodPos;
		blood.transform.SetParent(container, false);
		
		Animator bloodAnimator = blood.GetComponent<Animator> ();
		bloodAnimator.Play("BloodEnemyDie", 0, 0f);
	}

}
