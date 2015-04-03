using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	// Game constants
	public const float GRIVATY = -0.003f;

	public const float SMASH_GROUND_X = 1f;
	public const float SMASH_GROUND_Y = 1.5f;
	
	public const float SMASH_AIR_X = 1.5f;
	public const float SMASH_AIR_Y = -2f;

	public const float ENEMY_FLOOR_X = 1f;
	public const float ENEMY_FLOOR_Y = 1.5f;
	public const float ENEMY_FLY_Y = 2.5f;

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
