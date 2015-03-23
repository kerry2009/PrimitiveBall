using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillItemDisplay : SelectablItemDispaly {
	public SkillsPanel panel;
	public SkillItemConfig itemConfig;

	public Text numText;

	public void SetItemConfig(SkillItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
	}

	public override void OnClickItem () {
		panel.OnSelectSkill (this);
	}

}
