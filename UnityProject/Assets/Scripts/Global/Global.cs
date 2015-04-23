using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	// Game constants
	public const float GRIVATY = -0.3f;

	public const float SMASH_GROUND_X = 0.4f;
	public const float SMASH_GROUND_Y = 1.5f;
	
	public const float SMASH_AIR_X = 0.8f;
	public const float SMASH_AIR_Y = -2f;

	public const float Enemy_SPEED_RATE_MIN = 0.7f;
	public const float Enemy_SPEED_RATE_MAX = 0.98f;

	public const float bgFrontScrollX = 0.75f;
	public const float bgFrontScrollY = 1f;

	public const float bgMidScrollX = 0.45f;
	public const float bgMidScrollY = 0.25f;

	public const float bgBackScrollX = 0.2f;
	public const float bgBackScrollY = 0.1f;

	public const float bgStarsScrollX = 0.001f;
	public const float bgStarsScrollY = 0.0001f;

	public const int xpPerMile = 100;

	// instances
	private static GameSettings _gs = null;
	private static Player _player = null;
	private static GlobalNative _native = null;

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

	public static GlobalNative native {
		get {
			if (_native == null) {
				_native = new GlobalNative();
			}
			return _native;
		}
	}

}
