using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
	public int coins = 0;
	public int xp = 0;

	public int heroId = 1;
	public int weaponId = 1;
	public int booster1Id = -1;
	public int booster2Id = -1;
	public uint lastGenIndex = 0;

	public int copper = 0;
	public int silver = 0;
	public int gold = 0;

	public Dictionary<int, HeroData> heros;
}
