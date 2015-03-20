using UnityEngine;
using System.Collections;

public class NaviMapController : MonoBehaviour {
	public GameObject naviMapPanel;

	public HeroPanel heroPanel;
	public SkillsPanel skillPanel;

	// Use this for initialization
	void Start () {
		heroPanel.gameObject.SetActive (false);
		skillPanel.gameObject.SetActive (false);
	}

	public void OnClickPlay () {
 		Application.LoadLevel ("GameArena");
	}

	public void OnClickHero () {
		heroPanel.OpenPanel ();
		HideNaviMap ();
	}

	public void OnClickForge () {
		heroPanel.OpenPanel ();
		HideNaviMap ();
	}

	public void OnClickSkill () {
		skillPanel.OpenPanel ();
		HideNaviMap ();
	}

	public void ShowNaviMap () {
		naviMapPanel.SetActive (true);
	}

	public void HideNaviMap () {
		naviMapPanel.SetActive (false);
	}

}
