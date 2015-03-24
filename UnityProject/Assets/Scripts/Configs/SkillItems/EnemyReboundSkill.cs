using UnityEngine;
using System.Collections;

public class EnemyReboundSkill : SkillItemConfig {
	
	public override void ApplyEffectToPlayProperty (PlayProperties pp, int point) {
		pp._enemyRebound = levels[point];
	}
	
}