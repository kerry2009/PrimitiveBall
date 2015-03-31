using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	public void LoadNaviMapScene () {
		Application.LoadLevel ("NaviMap");
	}
}
