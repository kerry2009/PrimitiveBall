using UnityEngine;
using System.Collections;

public class PlayProperties {
	public HeroItemConfig hero = null;
	public WeaponItemConfig weapon = null;
	public BoosterItemConfig booster1 = null;
	public BoosterItemConfig booster2 = null;

	// Skills
	public float _hitPower;
	public float _criticalHit;
	public float _floorFriction;
	public float _enemyRebound;
	public float _enemyAppear;
	public float _hitCoolDown;
	public float _addXpCoins;

	public float GetFirstHitPower(float angle) {
		float p = WeaponPower * SmashPower;
		if (angle >= hero.criticalHitAngleMin && angle <= hero.criticalHitAngleMax) {
			p *= 1.5f;
		}
		return p;
	}

	public float SmashPower {
		get {
			return _hitPower + hero.hitPower;
		}
	}

	public float WeaponPower {
		get {
			float pow = 0;
			if (weapon) {
				pow += weapon.power;
			}
			if (booster1) {
				//pow += booster1.power;
			}
			if (booster2) {
				//pow += booster2.power;
			}
			return pow;
		}
	}

	public float FloorFriction {
		get {
			return _floorFriction;
		}
	}

	public float EnemyRebound {
		get {
			return _enemyRebound;
		}
	}

	public float EnemyAppear {
		get {
			return _enemyAppear;
		}
	}

	public float AddXpCoins {
		get {
			return _addXpCoins;
		}
	}

}
