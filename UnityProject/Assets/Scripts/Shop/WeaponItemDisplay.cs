using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponItemDisplay : SelectablItemDispaly {
	public WeaponPanel panel;
	public WeaponItemConfig itemConfig;

	public Image equipedBG;
	
	public void SetItemConfig(WeaponItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
		OnUnequip ();
	}

	public override void OnClickItem () {
		panel.OnSelectWeapon (this);
	}

	public void OnEquip() {
		HideSelectableBGs ();
		equipedBG.gameObject.SetActive (true);
	}

	public void OnUnequip() {
		equipedBG.gameObject.SetActive (false);
	}
	
}