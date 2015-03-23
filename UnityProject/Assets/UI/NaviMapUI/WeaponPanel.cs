using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponPanel : GridListPanel {
	public GridLayoutGroup gridLayout;
	public WeaponItemDisplay shopItemPrefab;
	public WeaponItemDisplay currentWeapon = null;

	// Use this for initialization
	void Start () {
	}
	
	protected override void CreateItems() {
		WeaponItemDisplay itemDisplay = null;
		
		foreach (WeaponItemConfig heroCfg in Global.gameSettings.weaponConfigs.Values) {
			itemDisplay = (WeaponItemDisplay)Instantiate(shopItemPrefab);
			itemDisplay.panel = this;
			itemDisplay.SetItemConfig(heroCfg);
			
			itemDisplay.transform.SetParent(listContainer, false);
		}
		
		if (itemDisplay != null) {
			listContainer.sizeDelta = new Vector2(gridLayout.cellSize.x * Global.gameSettings.heroConfigs.Keys.Count + 10f, 160f);
		}
	}
	public void OnSelectWeapon(WeaponItemDisplay selectedWeapon) {
		if (currentWeapon != selectedWeapon) {
			if (currentWeapon) {
				currentWeapon.OnUnselected();
			}
			
			currentWeapon = selectedWeapon;
			currentWeapon.OnSelected();
		}
	}

}
