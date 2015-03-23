using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
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
