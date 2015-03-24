using UnityEngine;
using System.Collections;

public class HitCoolDownSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._hitCoolDown = levels[point];
	}

}
