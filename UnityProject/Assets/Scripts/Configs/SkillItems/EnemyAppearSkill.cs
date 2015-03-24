using UnityEngine;
using System.Collections;

public class EnemyAppearSkill : SkillItemConfig {

	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._enemyAppear = levels[point];
	}

}
