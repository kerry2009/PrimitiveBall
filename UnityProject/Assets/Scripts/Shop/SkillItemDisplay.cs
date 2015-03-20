using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillItemDisplay : MonoBehaviour {
	public SkillItemConfig itemConfig;

	public Button clickButton;
	public Image placeHolder;
	public Image selectedBG;
	public Image unselectedBG;

	public void Start() {
		OnUnSelected ();
	}

	public void SetItemConfig(SkillItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
	}

	public void OnSelected() {
		selectedBG.gameObject.SetActive (true);
		unselectedBG.gameObject.SetActive (false);
	}

	public void OnUnSelected() {
		selectedBG.gameObject.SetActive (false);
		unselectedBG.gameObject.SetActive (true);
	}

}
