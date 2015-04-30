using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

	public void SetBulletTime(float timeScale, float time) {
		iTween.Stop ();
		iTween.ValueTo (gameObject, iTween.Hash("from", timeScale, "to", 1, "time", time, "onupdate", "UpdateTimeScale", "ignoretimescale", true));
	}
	
	private void UpdateTimeScale(float ts) {
		Time.timeScale = ts;
	}

}