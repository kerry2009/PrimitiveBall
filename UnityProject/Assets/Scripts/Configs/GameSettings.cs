using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings {
	public Dictionary<int, HeroItemConfig> heroConfigs;
	public Dictionary<int, WeaponItemConfig> weaponConfigs;
	public Dictionary<int, BoosterItemConfig> boosterConfigs;
	public Dictionary<int, SkillItemConfig> skillConfigs;
	public LevelConfig[] levelConfigs;

	public void initGameSettings() {
		HeroItemConfig[] heroAry = Resources.LoadAll<HeroItemConfig> ("Prefabs/HeroCfg");
		heroConfigs = new Dictionary<int, HeroItemConfig> ();
		foreach (HeroItemConfig heroCfg in heroAry) {
			heroConfigs.Add(heroCfg.id, heroCfg);
		}

		WeaponItemConfig[] weaponAry = Resources.LoadAll<WeaponItemConfig> ("Prefabs/WeaponCfg");
		weaponConfigs = new Dictionary<int, WeaponItemConfig> ();
		foreach (WeaponItemConfig weaponCfg in weaponAry) {
			weaponConfigs.Add(weaponCfg.id, weaponCfg);
		}

		BoosterItemConfig[] boosterAry = Resources.LoadAll<BoosterItemConfig> ("Prefabs/BoosterCfg");
		boosterConfigs = new Dictionary<int, BoosterItemConfig> ();
		foreach (BoosterItemConfig boosterCfg in boosterAry) {
			boosterConfigs.Add(boosterCfg.id, boosterCfg);
		}

		SkillItemConfig[] skillAry = Resources.LoadAll<SkillItemConfig> ("Prefabs/SkillCfg");
		skillConfigs = new Dictionary<int, SkillItemConfig> ();
		foreach (SkillItemConfig skillCfg in skillAry) {
			skillConfigs.Add(skillCfg.id, skillCfg);
		}

		levelConfigs = Resources.LoadAll<LevelConfig> ("Prefabs/LevelsCfg");
	}

}
