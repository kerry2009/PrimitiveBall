using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	// Game constants
	public const float GRIVATY = -0.01f;

	public const float SMASH_GROUND_X = 0.5f;
	public const float SMASH_GROUND_Y = 0.6f;
	
	public const float SMASH_AIR_X = 0.6f;
	public const float SMASH_AIR_Y = -1.0f;

	public const float ENEMY_FLOOR_X = 0.5f;
	public const float ENEMY_FLOOR_Y = 0.5f;
	public const float ENEMY_FLY_Y = 0.8f;

	// instances
	private static GameSettings _gs = null;
	private static Player _player = null;

	public static GameSettings gameSettings {
		get {
			if (_gs == null) {
				_gs = new GameSettings();
				_gs.initGameSettings();
			}
			return _gs;
		}
	}

	public static Player player {
		get {
			if (_player == null) {
				_player = new Player();
				_player.initPlayer();
			}
			return _player;
		}
	}

}
