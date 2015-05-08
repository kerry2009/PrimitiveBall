using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BloodManager : MonoBehaviour {
	public ParticleSystem geekTrailBloodParticle;
	public ParticleSystem hitFloorBloodParticle;
	public ParticleSystem hitEnemyBloodParticle;

	public BloodManager() {
	}

	public void AddHitFloorBlood(Vector3 bloodPos) {
		// create blood
		ParticleSystem bloodEffect = Instantiate(hitFloorBloodParticle, bloodPos, hitFloorBloodParticle.transform.rotation)  as ParticleSystem;

		bloodEffect.transform.SetParent(transform, false);
		bloodEffect.Play (true);
	}

	public void AddEnemyBlood(Enemy enemy) {
		// create blood
		ParticleSystem bloodEffect = Instantiate(hitEnemyBloodParticle, Vector3.zero, hitEnemyBloodParticle.transform.rotation)  as ParticleSystem;

		bloodEffect.transform.SetParent(enemy.transform, false);
		bloodEffect.Play (true);
	}

	public void AddGeekTrailBlood(Geek geek) {
		if (geek.GetComponentsInChildren<ParticleSystem>().Length < 3) {
			// create blood
			ParticleSystem bloodEffect = Instantiate(geekTrailBloodParticle, Vector3.zero, geekTrailBloodParticle.transform.rotation)  as ParticleSystem;
			
			bloodEffect.transform.SetParent(geek.transform, false);
			bloodEffect.Play (true);
		}

	}

}
