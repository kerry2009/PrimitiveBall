using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player {

	private PlayerData _playerData = null;
	private WeaponsData _weapons = null;
	private BoostersData _boosters = null;
	private SkillsData _skills = null;

	public PlayProperties playProperties;

	private string playerDataPath;
	private string weaponsDataPath;
	private string boostersDataPath;
	private string skillsDataPath;

	public const int CUSTOM_ID = 1000;

	public void initPlayer() {
		Debug.Log (Application.persistentDataPath);

		playProperties = new PlayProperties ();

		playerDataPath = Application.persistentDataPath + "/Player.gd";
		weaponsDataPath = Application.persistentDataPath + "/Weapons.gd";
		boostersDataPath = Application.persistentDataPath + "/Boosters.gd";
		skillsDataPath = Application.persistentDataPath + "/Skills.gd";

		LoadPlayerData ();
		LoadSkillsData ();
		LoadWeaponsData ();
		LoadBoostersData ();

		UpdatePlayPropertiesByPlayer ();
	}

	private void LoadData<DType>(string path, ref DType data) {
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open);
			data = (DType)bf.Deserialize (file);
			file.Close ();
		}
	}

	private void SaveData<DType>(string path, DType data) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (path);
		bf.Serialize(file, data);
		file.Close();
	}

	// Player Data
	private void LoadPlayerData() {
		LoadData<PlayerData>(playerDataPath, ref _playerData);
		CheckPlayerDataValidation ();
	}
	public void SavePlayerData() {
		SaveData<PlayerData>(playerDataPath, _playerData);
	}
	// ==============================================

	// Skills Data
	private void LoadSkillsData() {
		LoadData<SkillsData>(skillsDataPath, ref _skills);
		CheckSkillsDataValidation ();
	}
	public void SaveSkillsData() {
		UpdatePlayPropertiesBySkills ();
		SaveData<SkillsData>(skillsDataPath, _skills);
	}
	// ==============================================

	// Weapons Data
	private void LoadWeaponsData() {
		LoadData<WeaponsData>(weaponsDataPath, ref _weapons);
		CheckWeaponsValidation ();
	}
	public void SaveWeaponsData() {
		SaveData<WeaponsData>(weaponsDataPath, _weapons);
	}
	// ==============================================

	// Boosters Data
	private void LoadBoostersData() {
		LoadData<BoostersData>(boostersDataPath, ref _boosters);
		CheckBoostersValidation ();
	}
	public void SaveBoostersData() {
		SaveData<BoostersData>(boostersDataPath, _boosters);
	}
	// ==============================================

	private void CheckPlayerDataValidation () {
		bool shouldSavePlayerData = false;

		// Check heros data
		Dictionary<int, HeroItemConfig> heroConfigs = Global.gameSettings.heroConfigs;

		if (_playerData == null) {
			_playerData = new PlayerData();
			shouldSavePlayerData = true;
		}

		if (_playerData.heros == null) {
			_playerData.heros = new Dictionary<int, HeroData>();
			
			foreach (int heroId in heroConfigs.Keys) {
				_playerData.heros[heroId] = new HeroData();
			}

			shouldSavePlayerData = true;
		}

		// check if new hero config added
		foreach (int heroId in heroConfigs.Keys) {
			if (!_playerData.heros.ContainsKey(heroId)) {
				HeroData heroData = new HeroData();
				_playerData.heros[heroId] = heroData;
				shouldSavePlayerData = true;
			}
		}
		
		if (_playerData.heroId < 0) {
			_playerData.heroId = 1;
			shouldSavePlayerData = true;
		}

		// Check Weapon data
		if (_playerData.weaponId < 0) {
			_playerData.weaponId = 1;
			shouldSavePlayerData = true;
		}

		if (shouldSavePlayerData) {
			SavePlayerData();
		}
	}

	private void CheckSkillsDataValidation() {
		bool shoudSaveSkillsData = false;
		
		if (_skills == null) {
			shoudSaveSkillsData = true;
			_skills = new SkillsData();
		}

		Dictionary<int, SkillItemConfig> skillConfigs = Global.gameSettings.skillConfigs;
		if (_skills.list == null) {
			shoudSaveSkillsData = true;
			_skills.list = new Dictionary<int, SkillData>();

			foreach (int skillId in skillConfigs.Keys) {
				SkillData skillData = new SkillData();
				_skills.list.Add(skillId, skillData);
			}
		}

		// check if new hero config added
		foreach (int skillId in skillConfigs.Keys) {
			if (!_skills.list.ContainsKey(skillId)) {
				SkillData skillData = new SkillData();
				_skills.list.Add(skillId, skillData);
				shoudSaveSkillsData = true;
			}
		}

		if (shoudSaveSkillsData) {
			SaveSkillsData ();
		} else {
			UpdatePlayPropertiesBySkills ();
		}
	}

	private void UpdatePlayPropertiesBySkills() {
		// apply skills effections to play properties
		foreach (int skillId in _skills.list.Keys) {
			Global.gameSettings.skillConfigs[skillId].ApplyEffectToPlayProperty(playProperties, _skills.list[skillId].point);
		}
	}

	private void UpdatePlayPropertiesByPlayer() {
		playProperties.hero = Global.gameSettings.heroConfigs[_playerData.heroId];
		playProperties.weapon = Global.gameSettings.weaponConfigs[_playerData.weaponId];
		if (Global.gameSettings.boosterConfigs.ContainsKey(_playerData.booster1Id)) {
			playProperties.booster1 = Global.gameSettings.boosterConfigs[_playerData.booster1Id];
		}
		if (Global.gameSettings.boosterConfigs.ContainsKey(_playerData.booster2Id)) {
			playProperties.booster2 = Global.gameSettings.boosterConfigs[_playerData.booster2Id];
		}
	}

	private void CheckWeaponsValidation() {
		bool shouldSavePlayerData = false;
		bool shouldSaveWeaponsData = false;

		if (_weapons == null) {
			shouldSaveWeaponsData = true;
			_weapons = new WeaponsData();
		}

		if (_weapons.list == null) {
			shouldSaveWeaponsData = true;
			_weapons.list = new Dictionary<int, WeaponData>();
			WeaponData wd = new WeaponData();
			_weapons.list.Add(1, wd);
			_playerData.weaponId = 1;
		}

		if (!_weapons.list.ContainsKey(_playerData.weaponId)) {
			_playerData.weaponId = 1;
			shouldSavePlayerData = true;
		}

		if (shouldSaveWeaponsData) {
			SaveWeaponsData();
		}

		if (shouldSavePlayerData) {
			SavePlayerData();
		}
	}

	private void CheckBoostersValidation() {
		bool shouldSavePlayerData = false;
		bool shouldSaveBoostersData = false;

		if (_boosters == null) {
			shouldSaveBoostersData = true;
			_boosters = new BoostersData();
		}
		
		if (_boosters.list == null) {
			shouldSaveBoostersData = true;
			_boosters.list = new Dictionary<int, BoosterData>();
		}

		if (!_boosters.list.ContainsKey(_playerData.booster1Id)) {
			_playerData.booster1Id = -1;
			shouldSavePlayerData = true;
		}
		if (!_boosters.list.ContainsKey(_playerData.booster2Id)) {
			_playerData.booster2Id = -1;
			shouldSavePlayerData = true;
		}
		
		if (shouldSaveBoostersData) {
			SaveBoostersData();
		}

		if (shouldSavePlayerData) {
			SavePlayerData();
		}
	}

	public int coins {
		get {
			return _playerData.coins;
		}
		set {
			_playerData.coins = value;
		}
	}

	public int xp {
		get {
			return _playerData.xp;
		}
		set {
			_playerData.xp = value;
		}
	}

	public LevelConfig level {
		get {
			LevelConfig foundLevel = null;
			LevelConfig[] lvConfgs = Global.gameSettings.levelConfigs;
			for (int i = lvConfgs.Length - 1; i >= 0; i--) {
				if (_playerData.xp >= lvConfgs[i].xp) {
					foundLevel = lvConfgs[i];
					break;
				}
			}
			return foundLevel;
		}
	}

	public int copper {
		get {
			return _playerData.copper;
		}
		set {
			_playerData.copper = value;
			SavePlayerData();
		}
	}

	public int silver {
		get {
			return _playerData.silver;
		}
		set {
			_playerData.silver = value;
			SavePlayerData();
		}
	}

	public int gold {
		get {
			return _playerData.gold;
		}
		set {
			_playerData.gold = value;
			SavePlayerData();
		}
	}

	public void SetHeroById(int heroId) {
		if (_playerData.heros.ContainsKey(heroId)) {
			if (_playerData.heroId != heroId) {
				_playerData.heroId = heroId;
				playProperties.hero = Global.gameSettings.heroConfigs[_playerData.heroId];
				SavePlayerData();
			}
		} else {
			Debug.LogError("Invalidate Hero ID!");
		}
	}

	public int heroId {
		get {
			return _playerData.heroId;
		}
	}

	public void SetWeaponById(int weaponId) {
		if (_weapons.list.ContainsKey(weaponId)) {
			_playerData.weaponId = weaponId;
			playProperties.weapon = Global.gameSettings.weaponConfigs[_playerData.weaponId];
			SavePlayerData();
		} else {
			Debug.LogError("Invalidate Weapon ID!");
		}
	}

	public int weaponId {
		get {
			return _playerData.weaponId;
		}
	}

	public void SetBooster1ById(int boosterId) {
		if (_boosters.list.ContainsKey(boosterId)) {
			_playerData.booster1Id = boosterId;
		} else {
			Debug.LogError("Invalidate Booster1 ID!");
		}
	}

	public int booster1Id {
		get {
			return _playerData.booster1Id;
		}
	}

	public void SetBooster2ById(int boosterId) {
		if (_boosters.list.ContainsKey(boosterId)) {
			_playerData.booster2Id = boosterId;
		} else {
			Debug.LogError("Invalidate Booster2 ID!");
		}
	}

	public int booster2Id {
		get {
			return _playerData.booster2Id;
		}
	}

	public Dictionary<int, WeaponData> weapons {
		get {
			return _weapons.list;
		}
	}

	public Dictionary<int, BoosterData> boosters {
		get {
			return _boosters.list;
		}
	}

	public Dictionary<int, SkillData> skills {
		get {
			return _skills.list;
		}
	}

	public void SaveCostumeWeapon() {
	}

	public void SaveCostumeBooster() {
	}

}
