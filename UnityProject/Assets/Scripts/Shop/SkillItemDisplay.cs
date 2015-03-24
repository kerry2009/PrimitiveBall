using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillItemDisplay : SelectablItemDispaly {
	public SkillsPanel panel;
	public SkillItemConfig itemConfig;
	public SkillData data;

	public Text numText;

	public void SetItemConfig(SkillItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
	}

	protected override void InitItem() {
		if (data != null) {
			RefreshNum ();
		}
	}

	public override void OnClickItem () {
		panel.OnSelectSkill (this);
	}

	private void RefreshNum() {
		numText.text = data.point.ToString();
	}

	public void AddSkill() {
		if (data != null) {
			if (data.point + 1 < itemConfig.levels.Length) {
				data.point ++;
			}
			RefreshNum();
		}
	}

	public void RestSkill() {
		if (data != null) {
			data.point = 0;
			RefreshNum();
		}
	}

}
