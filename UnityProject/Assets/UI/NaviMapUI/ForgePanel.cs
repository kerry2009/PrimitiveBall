using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForgePanel : GenericPanel {
	public Text copperTxt;
	public Text silverTxt;
	public Text goldTxt;

	void Start() {
		RefreshMaterialsNum ();
	}

	private void RefreshMaterialsNum() {
		copperTxt.text = Global.player.copper.ToString();
		silverTxt.text = Global.player.silver.ToString();
		goldTxt.text = Global.player.gold.ToString();
	}

	public void MakeWeaponFromCopper() {
		Global.player.copper++;
		RefreshMaterialsNum ();
	}

	public void MakeWeaponFromSilver() {
		Global.player.silver++;
		RefreshMaterialsNum ();
	}

	public void MakeWeaponFromGold() {
		Global.player.gold++;
		RefreshMaterialsNum ();
	}
}
