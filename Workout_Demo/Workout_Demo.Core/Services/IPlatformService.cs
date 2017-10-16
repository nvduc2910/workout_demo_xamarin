using System;
namespace WorkoutDemo.Core
{
	public enum OS
	{
		Droid,
		Touch,
		WinPhone,
		WinStore,
		Mac,
		Wpf
	}

	public enum RepeatEachTime
	{
		None,
		Week
	}

	public interface IPlatformService
	{
		OS OS { get; }

		string GetDeviceUDID();

		void ShowNetworkIndicator();

		void HideNetworkIndicator();

		string GetPreference(string key);

		void SetPreference(string key, string value);

		void RegisterPushNotification();

		bool DetectInternerConnection();

		object ScheduleLocalNotification(int secs, string message);
		object ScheduleLocalNotificationRepeat(DateTime dateTime, string message, RepeatEachTime repeat);
		void RemoveAllLocalNotification();
		void RemoveLocalNotification(object localNotification);

		void SendEmail(string recipients, string subject);
		void ChangeAutoLockScreen(bool enable);

		void OpenWeb(string url);

	}
}
