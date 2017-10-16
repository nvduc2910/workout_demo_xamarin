using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using Foundation;
using UIKit;

namespace WorkoutDemo.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		// class-level declarations

		CustomPresenter mPresenter;
		Setup mSetup;

		public override UIWindow Window
		{
			get;
			set;
		}

		public CustomPresenter Presenter
		{
			get
			{
				return mPresenter;
			}
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			mPresenter = new CustomPresenter(this, Window);

			mSetup = new Setup(this, mPresenter);
			mSetup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			//      OneSignal.Current.StartInit("3ed7b75b-0e14-4d38-b46a-cbcd8850b79c")
			//.EndInit();

			//OneSignal.Current.RegisterForPushNotifications();

			//OneSignal.Current.IdsAvailable(IdsAvailable);

			//var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
			//	UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
			//	null);

			//UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);

			//UIApplication.SharedApplication.RegisterForRemoteNotifications();

			//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			Window.MakeKeyAndVisible();

			return true;
		}


		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			//// Get current device token
			//var DeviceToken = deviceToken.Description;
			//if (!string.IsNullOrWhiteSpace(DeviceToken))
			//{
			//	DeviceToken = DeviceToken.Trim('<').Trim('>');
			//}

			//// Get previous device token
			//var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

			//// Has the token changed?
			//if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
			//{
			//	//TODO: Put your own logic here to notify your server that the device token has changed/been created!
			//}
			//// Save new device token 
			//NSUserDefaults.StandardUserDefaults.SetString(DeviceToken.Replace(" ", ""), "PushDeviceToken");

			//         System.Console.WriteLine("Device token : " + DeviceToken.Replace(" ", ""));

			//Mvx.Resolve<ICacheService>().DeviceToken = DeviceToken.Replace(" ", "");

		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}
