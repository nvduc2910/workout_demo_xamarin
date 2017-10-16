using System;
using System.Diagnostics;
using Foundation;
using WorkoutDemo.Core;
using MvvmCross.Platform;
using UIKit;

namespace WorkoutDemo.iOS
{
	public class PlatformServices : IPlatformService
	{
		public PlatformServices()
		{
		}

		public OS OS
		{
			get
			{
				return OS.Touch;
			}
		}

		public void ChangeAutoLockScreen(bool enable)
		{
			UIApplication.SharedApplication.IdleTimerDisabled = !enable;
		}

		public bool DetectInternerConnection()
		{
			NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
			if (internetStatus == NetworkStatus.ReachableViaWiFiNetwork || internetStatus == NetworkStatus.ReachableViaCarrierDataNetwork)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public string GetDeviceUDID()
		{
			var udid = UIDevice.CurrentDevice.IdentifierForVendor.ToString();
			udid = udid.Substring(udid.IndexOf('>') + 1).Trim().Replace("-", "");
			return udid;
		}

		public string GetPreference(string key)
		{
			return NSUserDefaults.StandardUserDefaults.StringForKey(key);
		}

		public void HideNetworkIndicator()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

		public void OpenWeb(string url)
		{
			UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
		}

		public void RegisterPushNotification()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
									   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
									   new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();

			}
			else
			{
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}
		}

		public void RemoveAllLocalNotification()
		{
			UIApplication.SharedApplication.CancelAllLocalNotifications();
		}

		public void RemoveLocalNotification(object localNotification)
		{
			if (localNotification != null)
			{
				Console.WriteLine("UnSchedule LocalNotification");
				UILocalNotification tempLocalNotification = localNotification as UILocalNotification;
				if (tempLocalNotification != null)
				{
					UIApplication.SharedApplication.CancelLocalNotification(tempLocalNotification);
				}
			}
		}

		public object ScheduleLocalNotification(int secs, string message)
		{
			Console.WriteLine("Schedule LocalNotification");
			UILocalNotification localNotification = new UILocalNotification();
			localNotification.SoundName = UILocalNotification.DefaultSoundName.ToString();

			NSDate date = NSDate.FromTimeIntervalSinceNow(secs);
			Debug.WriteLine("date :" + date.ToString());
			localNotification.FireDate = date;
			localNotification.AlertBody = message;

			UIApplication.SharedApplication.ScheduleLocalNotification(localNotification);
			return localNotification;
		}

		public object ScheduleLocalNotificationRepeat(DateTime dateTime, string message, RepeatEachTime repeat)
		{
			UILocalNotification localNotification = new UILocalNotification();
			localNotification.SoundName = UILocalNotification.DefaultSoundName.ToString();

			NSDate date = dateTime.DateTimeToNSDate();
			Debug.WriteLine("date :" + date.ToString());
			localNotification.FireDate = date;
			localNotification.AlertBody = message;

			switch (repeat)
			{
				case RepeatEachTime.Week:
					localNotification.RepeatInterval = NSCalendarUnit.Week;
					break;
				default:
					break;
			}

			UIApplication.SharedApplication.ScheduleLocalNotification(localNotification);
			return localNotification;
		}

		public void SendEmail(string recipients, string subject)
		{
			string deeplink = "mailto:" + recipients + "?subject=" + subject.Replace(" ", "%20");

			NSUrl url = new NSUrl(deeplink);
			if (UIApplication.SharedApplication.CanOpenUrl(url))
			{
				UIApplication.SharedApplication.OpenUrl(url);
			}
			else
			{
				Mvx.Resolve<IMessageboxService>().Show("Not supported", "Mail is not supported, or not configured on this device.");
			}
		}

		public void SetPreference(string key, string value)
		{
			NSUserDefaults.StandardUserDefaults.SetString(value, key);
		}

		public void ShowNetworkIndicator()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}
	}
}
