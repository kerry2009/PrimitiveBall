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

	private int currentCoins;
	private int targetCoins;

	void Awake() {
		currentCoins = 0;
		targetCoins = 0;
		criticalPowerReady.gameObject.SetActive (false);
	}

	public void AddCoinsTo(int _targetCoins) {
		targetCoins = _targetCoins;

		//iTween will ignore additional calls on a gameObject that have all of the same properties so we need to interrupt current iTweens:
		//Remember: iTween follows the "Doesn't Destroy Duplicates" approach.
		iTween.Stop(hudCoinsText.gameObject);
		
		//increment the score:
		iTween.ValueTo(hudCoinsText.gameObject, iTween.Hash("from", currentCoins, "to", targetCoins, "time", 1, "onupdate", "UpdateCoinsDisplay", "onupdatetarget", gameObject));
	}

	private void UpdateCoinsDisplay(int newCoins) {
		currentCoins = newCoins;
		hudCoinsText.text = newCoins.ToString();
	}

	// Update is called once per frame
	void LateUpdate () {
		hudSpeedText.text = ((int)(geek.speedX)).ToString() + " m/h";
		hudDistanceText.text = ((int)(geek.Distance)).ToString() + " m";
		hudHeightText.text = ((int)(geek.Height)).ToString() + " m";

		if (targetCoins != Global.player.coins) {
			AddCoinsTo (Global.player.coins);
		}
	}

}
