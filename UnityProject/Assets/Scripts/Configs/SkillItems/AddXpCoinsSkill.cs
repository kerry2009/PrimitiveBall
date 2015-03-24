using UnityEngine;
using System.Collections;

public class AddXpCoinsSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._addXpCoins = levels[point];
	}

}
