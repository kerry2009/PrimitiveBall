using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArenaHUD : MonoBehaviour {
	public Text hudSpeedText;
	public Text hudDistanceText;
	public Text hudHeightText;

	public Image criticalPowerCD;
	public Image criticalPowerReady;

	public Geek geek;

	void Start() {
		criticalPowerReady.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		Vector3 geekPos = geek.transform.position;
		hudSpeedText.text = ((int)(geek.speedX * 100)).ToString() + " m/h";
		hudDistanceText.text = ((int)(geekPos.x * 100)).ToString() + " m";
		hudHeightText.text = ((int)(geekPos.y * 100)).ToString() + " m";
	}

}
