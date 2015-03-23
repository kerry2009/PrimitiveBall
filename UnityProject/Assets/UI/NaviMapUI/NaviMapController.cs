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

}
