using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
	public int coins = 0;

	public string heroId;
	public string weaponId;
	public string booster1Id;
	public string booster2Id;

	public int[] weapons;
	public int[] heros;
	public int[] boosters;
}
