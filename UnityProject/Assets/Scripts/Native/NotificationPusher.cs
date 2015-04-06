using UnityEngine;
using System.Collections;
using System;

public class NotificationPusher {

	public static void NotificationMessage(string message, int hour, bool isRepeatDay) {
		int year = DateTime.Now.Year;
		int month = DateTime.Now.Month;
		int day= DateTime.Now.Day;
		DateTime newDate = new DateTime(year, month, day, hour, 0, 0);

		NotificationMessage(message, newDate, isRepeatDay);
	}

	public static void NotificationMessage(string message, DateTime newDate, bool isRepeatDay) {
		#if UNITY_IOS
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(
			UnityEngine.iOS.NotificationType.Alert | 
			UnityEngine.iOS.NotificationType.Badge | 
			UnityEngine.iOS.NotificationType.Sound);

		if (newDate > DateTime.Now) {
			UnityEngine.iOS.LocalNotification lNotification = new UnityEngine.iOS.LocalNotification();
			lNotification.fireDate = newDate; 
			lNotification.alertBody = message;
			lNotification.applicationIconBadgeNumber = 1;
			lNotification.hasAction = true;
			if (isRepeatDay) {
				lNotification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.GregorianCalendar;
				lNotification.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
			}
			lNotification.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
			UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(lNotification);
		}
		#endif
	}

	public static void OnApplicationPause(bool paused) {
		if (paused) {
			// NotificationMessage("Welcome to play primitive ball every day in 10 sec", DateTime.Now.AddSeconds(10), false);
			// NotificationMessage("Welcome to play primitive ball every day", 12, true);
		} else {
			CleanNotification();
		}
	}

	public static void CleanNotification() {
		#if UNITY_IOS
		UnityEngine.iOS.LocalNotification lNotification = new UnityEngine.iOS.LocalNotification ();
		lNotification.applicationIconBadgeNumber = -1;
		UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow (lNotification);
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications ();
		UnityEngine.iOS.NotificationServices.ClearLocalNotifications ();
		#endif
	}

}
