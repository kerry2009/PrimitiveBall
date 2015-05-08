using UnityEngine;
using System.Collections;

public class DestroyThisTimed : MonoBehaviour {
	public float destroyTime = 3f;

	void Start () {
		Destroy (gameObject, destroyTime);
	}

}