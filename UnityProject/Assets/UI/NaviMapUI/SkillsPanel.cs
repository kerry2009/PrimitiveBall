using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillsPanel : GenericPanel {
	public Transform itemsListContainer;
	public SkillItemDisplay shopItemPrefab;

	protected override void CreateItems() {
		SkillItemDisplay itemDisplay = null;

		Dictionary<int, SkillItemConfig> skillConfigs = Global.gameSettings.skillConfigs;
		foreach (SkillItemConfig skillCfg in skillConfigs.Values) {
			itemDisplay = (SkillItemDisplay)Instantiate(shopItemPrefab);
			itemDisplay.SetItemConfig(skillCfg);

			itemDisplay.transform.SetParent(itemsListContainer, false);
		}
	}

}
