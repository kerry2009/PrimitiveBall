using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroItemDisplay : MonoBehaviour {
	public HeroItemConfig itemConfig;
	
	public Button clickButton;
	public Image placeHolder;
	public Image background;
	
	private static Color normalColor = new Color (1.0f, 1.0f, 1.0f);
	private static Color grayColor = new Color (0.5f, 0.5f, 0.5f);
	
	public void Start() {
	}
	
	public void SetItemConfig(HeroItemConfig ic) {
		itemConfig = ic;
		placeHolder.sprite = itemConfig.shopIcon;
	}
	
	public void OnSelected() {
		background.color = normalColor;
		placeHolder.color = normalColor;
	}
	
	public void OnUnSelected() {
		background.color = grayColor;
		placeHolder.color = grayColor;
	}
	
}