using UnityEngine;
using System.Collections;

public class AngleMeter : Meter {
	private float speed;
	private float angleMin;
	private float angleMax;

	public void InitMeter(float _speed, float _angleMin, float _angleMax) {
		speed = _speed;
		angleMin = _angleMin;
		angleMax = _angleMax;
	}

	public override void CursorRun() {
		TweenLite.To (cursor.transform, TweenProperties.localRotation,
		              new Vector3 (0, 0, angleMin),
		              new Vector3 (0, 0, angleMax),
		              speed, TweenRestricts.z, null, null, true);
	}

	public override float GetPercentage() {
		return cursor.transform.eulerAngles.z;
	}

	public override void StopRunCursor() {
		TweenLite.Kill (cursor.transform, TweenProperties.localRotation);
	}

}
