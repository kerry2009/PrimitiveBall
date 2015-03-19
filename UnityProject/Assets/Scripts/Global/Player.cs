using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player {

	public PlayerData playerData;

	private int _coins;
	private WeaponItem _weapon;
	private BoosterItem _booster1;
	private BoosterItem _booster2;

	public void initPlayer() {
		LoadPlayerData ();
	}
	
	private void LoadPlayerData() {
		if (File.Exists (Application.persistentDataPath + "/Player.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Player.gd", FileMode.Open);
			playerData = (PlayerData)bf.Deserialize (file);
			file.Close ();
		} else {
			playerData = new PlayerData();
			SavePlayerData();
		}
	}
	
	public void SavePlayerData() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/Player.gd");
		bf.Serialize(file, playerData);
		file.Close();
	}


}
