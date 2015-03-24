using UnityEngine;
using System.Collections;

public class FloorFrictionSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._floorFriction = levels[point];
	}

}
