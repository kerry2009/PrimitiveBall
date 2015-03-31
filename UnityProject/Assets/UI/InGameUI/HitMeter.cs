using UnityEngine;
using System.Collections;

public class HitMeter : MonoBehaviour {
	public Transform cursor;
	public Transform showPosTrans;
	public Transform hidePosTrans;

	private float speed;
	private float angleMin;
	private float angleMax;
	public Transform powerBG;

	public void InitMeter(float _speed, float _angleMin, float _angleMax) {
		speed = _speed;
		angleMin = _angleMin;
		angleMax = _angleMax;
	}

	public void RunAngleCursor() {
		TweenLite.To (cursor.transform, TweenProperties.localRotation,
		              new Vector3 (0, 0, angleMin),
		              new Vector3 (0, 0, angleMax),
		              speed, TweenRestricts.z, null, null, true);
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
		return powerBG.transform.localScale.x;
	}

	public void StopPowerCursor() {
		TweenLite.Kill (powerBG, TweenProperties.localScale);
	}

	public void easeIn() {
		TweenLite.To (transform, TweenProperties.position, transform.position, new Vector3 (transform.position.x, showPosTrans.position.y, transform.position.z), 0.7f, TweenRestricts.y, null, null, false);
	}
	
	public void easeOut() {
		TweenLite.To (transform, TweenProperties.position, transform.position, new Vector3 (transform.position.x, hidePosTrans.position.y, transform.position.z), 0.7f, TweenRestricts.y, null, null, false);
	}

}
