using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;

public class GlobalNative {
	public const string Ad_ID = "29338";

	private bool isSignedGameCenterer;
	private bool isGameCenterSigning;
	private bool isLeaderBoard;

	public GlobalNative() {
		isLeaderBoard = false;
		isSignedGameCenterer = false;
		isGameCenterSigning = false;
	}

	public void InitNatives() {
		if (Advertisement.isSupported) {
			Debug.Log("Advertisement is Supported!");
			Advertisement.Initialize(Ad_ID, false);
		}
	}

	public void LoginGameCenter() {
		if (!isGameCenterSigning) {
			if (!isSignedGameCenterer) {
				isGameCenterSigning = true;
				Social.localUser.Authenticate (GameCenterAuthenticated);
			}
		}
	}

	public void ShowLeaderBoard() {
		isLeaderBoard = true;
		if (!isGameCenterSigning && isSignedGameCenterer) {
			Social.ShowLeaderboardUI();
		}
	}

	public void RecoredScore(long score, string leaderBoardGroupId) {
		Social.ReportScore (score, leaderBoardGroupId, null);
	}

	private void GameCenterAuthenticated (bool success) {
		isSignedGameCenterer = success;
		isGameCenterSigning = false;

		if (isSignedGameCenterer) {
			if (isLeaderBoard) {
				ShowLeaderBoard ();
			}
			Debug.Log ("Authenticated GameCenter!");
		} else {
			Debug.Log ("Failed to authenticate GameCenter!");
		}
	}

}
