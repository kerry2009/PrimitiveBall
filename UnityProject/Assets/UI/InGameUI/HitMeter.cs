using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitMeter : MonoBehaviour {
	public Transform cursor;
	public Image cursorImg;
	public Transform showPosTrans;
	public Transform hidePosTrans;

	private float speed;
	private float angleMin;
	private float angleMax;
	private float criticalAngleMin;
	private float criticalAngleMax;
	private bool isAngleRun;

	public Transform powerBG;

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

		TweenLite.To (cursor.transform, TweenProperties.localRotation,
		              new Vector3 (0, 0, angleMin),
		              new Vector3 (0, 0, angleMax),
		              speed, TweenRestricts.z, null, null, true);
	}

	void Update() {
		if (isAngleRun) {
			float curAngle = cursor.transform.localRotation.z * 180 / Mathf.PI;
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
		TweenLite.Kill (cursor.transform, TweenProperties.localRotation);
	}

	public void RunPowerCursor() {
		TweenLite.To (powerBG, TweenProperties.localScale,
		              new Vector3 (0, 0, 1f),
		              new Vector3 (1f, 1f, 1f),
		              speed, TweenRestricts.x | TweenRestricts.y, null, null, true);
	}
	
	public float GetPowerPercentage() {
		return powerBG.localScale.x;
	}

	public void StopPowerCursor() {
		TweenLite.Kill (powerBG, TweenProperties.localScale);
	}

	public void easeIn() {
		TweenLite.To (transform, TweenProperties.position, transform.position, new Vector3 (transform.position.x, showPosTrans.position.y, transform.position.z), 0.7f, TweenRestricts.y, null, null, false);
	}
	
	public void easeOut() {
		isAngleRun = false;
		TweenLite.To (transform, TweenProperties.position, transform.position, new Vector3 (transform.position.x, hidePosTrans.position.y, transform.position.z), 0.7f, TweenRestricts.y, null, null, false);
	}

}
