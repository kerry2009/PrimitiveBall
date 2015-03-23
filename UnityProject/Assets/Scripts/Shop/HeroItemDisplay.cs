using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroItemDisplay : SelectablItemDispaly {
	public HeroPanel panel;
	public HeroItemConfig itemConfig;
	
	public void SetItemConfig(HeroItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
	}

	public override void OnClickItem () {
		panel.OnSelectHero (this);
	}
	
}