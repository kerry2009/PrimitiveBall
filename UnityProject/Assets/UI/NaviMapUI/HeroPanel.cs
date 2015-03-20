using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeroPanel : GenericPanel {
	public RectTransform itemsListContainer;
	public GridLayoutGroup gridLayout;
	public HeroItemDisplay shopItemPrefab;

	// Use this for initialization
	void Start () {
	}

	protected override void CreateItems() {
		HeroItemDisplay itemDisplay = null;

		foreach (HeroItemConfig heroCfg in Global.gameSettings.heroConfigs.Values) {
			itemDisplay = (HeroItemDisplay)Instantiate(shopItemPrefab);
			itemDisplay.SetItemConfig(heroCfg);

			itemDisplay.transform.SetParent(itemsListContainer, false);
		}

		if (itemDisplay != null) {
			itemsListContainer.sizeDelta = new Vector2(gridLayout.cellSize.x * Global.gameSettings.heroConfigs.Keys.Count + 10f, 160f);
		}
	}

	protected override void DestoryItems() {
		GameObject childObject;
		int numChildren = itemsListContainer.transform.childCount;
		for (int i = 0; i < numChildren; i++) {
			childObject = itemsListContainer.transform.GetChild(i).gameObject;
			Destroy(childObject);
		}
	}

}
