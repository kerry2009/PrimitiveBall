using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectablItemDispaly : MonoBehaviour {
	public const int STATUS_SELECTED = 1;
	public const int STATUS_UNSELECTED = 2;

	public Button clickButton;

	public Image placeHolder;
	public Image selectedBG;
	public Image unselectedBG;
	protected int status = STATUS_UNSELECTED;

	void Start() {
		if (clickButton) {
			clickButton.onClick.AddListener(() => ButtonClickItem());
		}

		if (status == STATUS_SELECTED) {
			OnSelected ();
		} else if (status == STATUS_UNSELECTED) {
			OnUnselected ();
		} else {
			HideSelectableBGs();
		}

		InitItem ();
	}

	protected virtual void InitItem() {
	}

	private void ButtonClickItem() {
		OnClickItem ();
	}

	public virtual void OnClickItem() {
	}

	public void OnSelected() {
		status = STATUS_SELECTED;
		selectedBG.gameObject.SetActive (true);
		unselectedBG.gameObject.SetActive (false);
	}

	public void OnUnselected() {
		status = STATUS_UNSELECTED;
		selectedBG.gameObject.SetActive (false);
		unselectedBG.gameObject.SetActive (true);
	}

	public void HideSelectableBGs() {
		status = STATUS_UNSELECTED;
		selectedBG.gameObject.SetActive (false);
		unselectedBG.gameObject.SetActive (false);
	}

	void OnDestroy () {
		if (clickButton) {
			clickButton.onClick.RemoveAllListeners ();
		}
	}

}
