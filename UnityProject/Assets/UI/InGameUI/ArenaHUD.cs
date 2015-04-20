using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArenaHUD : MonoBehaviour {
	public Text hudSpeedText;
	public Text hudDistanceText;
	public Text hudHeightText;
	public Text hudCoinsText;

	public Image criticalPowerCD;
	public Image criticalPowerReady;

	public Geek geek;

	void Start() {
		criticalPowerReady.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void LateUpdate () {
		hudSpeedText.text = ((int)(geek.speedX)).ToString() + " m/h";
		hudDistanceText.text = ((int)(geek.Distance)).ToString() + " m";
		hudHeightText.text = ((int)(geek.Height)).ToString() + " m";
		hudCoinsText.text = Global.player.coins.ToString();
	}

}
