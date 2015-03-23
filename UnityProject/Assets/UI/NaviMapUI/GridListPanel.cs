using UnityEngine;
using System.Collections;

public class GridListPanel : GenericPanel {
	public RectTransform listContainer;

	override protected void DestoryItems() {
		GameObject childObject;
		int numChildren = listContainer.childCount;
		for (int i = 0; i < numChildren; i++) {
			childObject = listContainer.GetChild(i).gameObject;
			Destroy(childObject);
		}
	}

}
