using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillsPanel : GridListPanel {
	public SkillItemDisplay shopItemPrefab;

	private SkillItemDisplay currentSkill = null;

	protected override void CreateItems() {
		SkillItemDisplay itemDisplay = null;

		Dictionary<int, SkillItemConfig> skillConfigs = Global.gameSettings.skillConfigs;
		foreach (SkillItemConfig skillCfg in skillConfigs.Values) {
			itemDisplay = (SkillItemDisplay)Instantiate(shopItemPrefab);
			itemDisplay.panel = this;

			itemDisplay.SetItemConfig(skillCfg);
			itemDisplay.transform.SetParent(listContainer, false);

			if (Global.player.skills.ContainsKey(skillCfg.id)) {
				itemDisplay.data = Global.player.skills[skillCfg.id];
			} else {
				Debug.LogError("Skill " + skillCfg.id + " not found!");
			}

			if (currentSkill == null) {
				currentSkill = itemDisplay;
				currentSkill.OnSelected();
			}
		}
	}

	public void OnSelectSkill(SkillItemDisplay selectedSkill) {
		if (currentSkill != selectedSkill) {
			if (currentSkill) {
				currentSkill.OnUnselected();
			}

			currentSkill = selectedSkill;
			currentSkill.OnSelected();
		}
	}

	public void ClickAddSkill() {
		if (currentSkill) {
			currentSkill.AddSkill();
			Global.player.SaveSkillsData();
		}
	}

	public void ClickResetSkills() {
		SkillItemDisplay itemDisplay;
		int numChildren = listContainer.childCount;
		for (int i = 0; i < numChildren; i++) {
			itemDisplay = listContainer.GetChild(i).GetComponent<SkillItemDisplay>();
			itemDisplay.RestSkill();
		}

		Global.player.SaveSkillsData();
	}

}
