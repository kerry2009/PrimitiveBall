using UnityEngine;
using System.Collections;

public class GenericPanel : MonoBehaviour {
	public virtual void OpenPanel() {
		gameObject.SetActive (true);
		CreateItems ();
	}

	protected virtual void CreateItems() {
	}

	public virtual void ClosePanel() {
		DestoryItems ();
		gameObject.SetActive (false);
	}

	protected virtual void DestoryItems() {
	}

}
