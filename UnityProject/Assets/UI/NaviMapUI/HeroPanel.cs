using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeroPanel : GridListPanel {
	public GridLayoutGroup gridLayout;
	public HeroItemDisplay shopItemPrefab;

	private HeroItemDisplay currentHero = null;

	// Use this for initialization
	void Start () {
	}

	protected override void CreateItems() {
		HeroItemDisplay itemDisplay = null;

		foreach (HeroItemConfig heroCfg in Global.gameSettings.heroConfigs.Values) {
			itemDisplay = (HeroItemDisplay)Instantiate(shopItemPrefab);
			itemDisplay.panel = this;
			itemDisplay.SetItemConfig(heroCfg);

			itemDisplay.transform.SetParent(listContainer, false);

			if (Global.player.heroId == heroCfg.id) {
				currentHero = itemDisplay;
				currentHero.OnSelected();
			}
		}

		if (itemDisplay != null) {
			listContainer.sizeDelta = new Vector2(gridLayout.cellSize.x * Global.gameSettings.heroConfigs.Keys.Count + 10f, 160f);
		}
	}

	public void OnSelectHero(HeroItemDisplay selectedHero) {
		if (currentHero != selectedHero) {
			if (currentHero) {
				currentHero.OnUnselected();
			}

			currentHero = selectedHero;
			currentHero.OnSelected();

			Global.player.SetHeroById(currentHero.itemConfig.id);
		}
	}

}
