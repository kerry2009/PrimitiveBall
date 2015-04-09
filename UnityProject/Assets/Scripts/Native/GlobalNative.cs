using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;
using System.Runtime.InteropServices;

public class GlobalNative {

	[DllImport("__Internal")]
	extern static public void popupAlert(string message);

	public const string Ad_ID = "29338";

	private bool isSignedGameCenterer;
	private bool isGameCenterSigning;
	private bool isLeaderBoard;

	public GlobalNative() {
		isLeaderBoard = false;
		isSignedGameCenterer = false;
		isGameCenterSigning = false;
	}

	public void InitUnityAds() {
		if (Advertisement.isSupported) {
			Debug.Log("Advertisement is Supported!");
			Advertisement.Initialize(Ad_ID, false);
		}
	}

	public void LoginGameCenter() {
		if (!isGameCenterSigning) {
			if (!isSignedGameCenterer) {
				isGameCenterSigning = true;
				Social.localUser.Authenticate (OnGameCenterAuthenticated);
			}
		}
	}

	public void PushNotification() {
		NotificationPusher.NotificationMessage("Welcome to play primitive ball every day in 10 sec", System.DateTime.Now.AddSeconds(10), false);
	}

	public void ShowAds() {
		Advertisement.Show ();
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

	private void OnGameCenterAuthenticated (bool success) {
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

	public void PopupAlert(string msg) {
		// pop alert
		popupAlert (msg);
	}

}
