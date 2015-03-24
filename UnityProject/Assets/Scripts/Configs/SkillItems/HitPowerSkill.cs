using UnityEngine;
using System.Collections;

public class HitPowerSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._hitPower = levels[point];
	}

}
