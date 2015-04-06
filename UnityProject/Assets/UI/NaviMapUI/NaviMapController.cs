using UnityEngine;
using System.Collections;

public class NaviMapController : MonoBehaviour {
	public GameObject naviMapPanel;

	public HeroPanel heroPanel;
	public SkillsPanel skillPanel;
	public WeaponPanel weaponPanel;
	public ForgePanel forgePanel;

	// Use this for initialization
	void Start () {
		heroPanel.gameObject.SetActive (false);
		skillPanel.gameObject.SetActive (false);
		weaponPanel.gameObject.SetActive (false);
		forgePanel.gameObject.SetActive (false);

		Global.native.InitUnityAds ();
		Global.native.LoginGameCenter ();
	}

	public void OnClickPlay () {
 		Application.LoadLevel ("GameArena");
	}

	public void ShowNaviMap () {
		naviMapPanel.SetActive (true);
	}

	public void HideNaviMap () {
		naviMapPanel.SetActive (false);
	}

	public void ShowAds() {
		Global.native.ShowAds ();
	}

	public void ShowLeaderboard() {
		Global.native.ShowLeaderBoard ();
	}

	public void PopupNativeAlert() {
		Global.native.PopupAlert ("Hello from primitive ball!");
	}

	public void PushNotification() {
		Global.native.PushNotification ();
	}

}
