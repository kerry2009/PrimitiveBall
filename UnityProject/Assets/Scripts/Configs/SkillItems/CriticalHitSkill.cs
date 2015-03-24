using UnityEngine;
using System.Collections;

public class CriticalHitSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._criticalHit = levels[point];
	}

}
