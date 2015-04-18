using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	// Game constants
	public const float GRIVATY = -0.06f;

	public const float SMASH_GROUND_X = 0.4f;
	public const float SMASH_GROUND_Y = 1.5f;
	
	public const float SMASH_AIR_X = 0.8f;
	public const float SMASH_AIR_Y = -2f;

	public const float bgFrontScrollX = 0.1f;
	public const float bgFrontScrollY = 1.0f;

	public const float bgMidScrollX = 0.025f;
	public const float bgMidScrollY = 0.025f;

	public const float bgBackScrollX = 0.01f;
	public const float bgBackScrollY = 0.01f;

	public const float bgStarsScrollX = 0.0001f;
	public const float bgStarsScrollY = 0.0001f;

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
