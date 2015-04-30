using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitMeter : MonoBehaviour {
	public GameObject cursor;
	public GameObject powerBG;
	public Image cursorImg;
	public Transform showPosTrans;
	public Transform hidePosTrans;

	private float speed;
	private float angleMin;
	private float angleMax;
	private float criticalAngleMin;
	private float criticalAngleMax;
	private bool isAngleRun;

	void Awake() {
		isAngleRun = false;
	}

	public void InitMeter(float _speed, float _angleMin, float _angleMax, float _criticalAngleMin, float _criticalAngleMax) {
		speed = _speed;
		angleMin = _angleMin;
		angleMax = _angleMax;
		criticalAngleMin = _criticalAngleMin;
		criticalAngleMax = _criticalAngleMax;
	}

	public void RunAngleCursor() {
		isAngleRun = true;

		Quaternion minRot = cursor.transform.localRotation;
		minRot.z = angleMin * Mathf.PI / 180;
		cursor.transform.localRotation = minRot;

		iTween.RotateTo (cursor, iTween.Hash ("z", angleMax, "time", speed, "loopType", iTween.LoopType.pingPong, "isLocal", true, "easetype", iTween.EaseType.linear, "onupdate", "OnRotationUpdate", "onupdatetarget", gameObject));
	}

	private void OnRotationUpdate() {
		if (isAngleRun) {
			float curAngle = ((int)(cursor.transform.localRotation.eulerAngles.z * 100f)) / 100f;

			if (curAngle >= criticalAngleMin && curAngle <= criticalAngleMax) {
				cursorImg.color = Color.grey;
			} else {
				cursorImg.color = Color.white;
			}
		}
	}
	
	public float GetAnglePercentage() {
		return cursor.transform.eulerAngles.z;
	}

	public void StopAngleCursor() {
		iTween.Stop (cursor);
	}

	public void RunPowerCursor() {
		Vector3 minSal = powerBG.transform.localScale;
		minSal.x = 0;
		minSal.y = 0;
		powerBG.transform.localScale = minSal;

		iTween.ScaleTo (powerBG, iTween.Hash ("x", 1, "y", 1, "time", speed, "loopType", iTween.LoopType.pingPong, "isLocal", true, "easetype", iTween.EaseType.linear));
	}
	
	public float GetPowerPercentage() {
		return powerBG.transform.localScale.x;
	}

	public void StopPowerCursor() {
		iTween.Stop (powerBG);
	}

	public void easeIn() {
		iTween.MoveTo (gameObject, iTween.Hash("y", showPosTrans.position.y, "time", 1, "easetype", iTween.EaseType.easeOutBounce));
	}
	
	public void easeOut() {
		isAngleRun = false;
		iTween.MoveTo (gameObject, iTween.Hash("y", hidePosTrans.position.y, "time", 1, "easetype", iTween.EaseType.easeInElastic));
	}

}
