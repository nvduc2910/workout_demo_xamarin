using System;
using System.Threading.Tasks;
using WorkoutDemo.Core;
using MBProgressBinding;
using MessageBar;
using UIKit;

namespace WorkoutDemo.iOS
{
	public class MessageboxService : IMessageboxService
	{
		private UIAlertView alertView;

		private bool CheckIfiOS8()
		{
			return UIDevice.CurrentDevice.CheckSystemVersion(8, 0);
		}
		public MessageboxService()
		{
		}

		public void Show(string title, string message)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
			{
				if (CheckIfiOS8())
				{
					UIAlertController alertViewController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
					UIAlertAction okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, a =>
					{
					});
					alertViewController.AddAction(okAction);

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alertViewController, true, null);
				}
				else
				{
					alertView = new UIAlertView(title, message, null, "OK", null);
					alertView.Show();
					alertView.Dismissed += (sender, e) =>
					{
						alertView.Dispose();
						alertView = null;
					};
				}
			}));
		}

		public Task<int> ShowThreeOptions(string title, string message, string cancelOption, string firstOption, string secondOption)
		{
			var tcs = new TaskCompletionSource<int>();

			UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
			{
				if (CheckIfiOS8())
				{
					UIAlertController alertViewController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
					UIAlertAction cancelAction = UIAlertAction.Create(cancelOption, UIAlertActionStyle.Cancel, a => tcs.SetResult(0));
					alertViewController.AddAction(cancelAction);

					UIAlertAction firstlAction = UIAlertAction.Create(firstOption, UIAlertActionStyle.Default, a => tcs.SetResult(1));
					alertViewController.AddAction(firstlAction);

					UIAlertAction secondAction = UIAlertAction.Create(secondOption, UIAlertActionStyle.Default, a => tcs.SetResult(2));
					alertViewController.AddAction(secondAction);

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alertViewController, true, null);
				}
				else
				{
					UIAlertView alert = new UIAlertView(title, message, null, cancelOption, firstOption, secondOption);
					alert.Clicked += (sender, buttonArgs) => tcs.TrySetResult((int)buttonArgs.ButtonIndex);
					alert.Dismissed += (sender, e) =>
					{
						alert.Dispose();
						alert = null;
					};
					alert.Show();
				}
			}));

			return tcs.Task;
		}

		public void ShowToast(string message, int timeoutMs = 1500)
		{
			UIWindow window = ((AppDelegate)UIApplication.SharedApplication.Delegate).Window;

			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				MBProgressHUD hud = MBProgressHUD.ShowHUDAddedTo(window, true);

				hud.Mode = MBProgressHUDMode.Text;
				hud.DetailsLabel.Text = message;
				hud.RemoveFromSuperViewOnHide = true;
				hud.Margin = 10f;

				hud.BezelView.BackgroundColor = UIColor.Black;
				hud.DetailsLabel.TextColor = UIColor.White;

				hud.HideAnimated(true, timeoutMs * 1.0 / 1000);
			});
		}

		public Task<bool> ShowTwoOptions(string title, string message, string cancelOption, string yesOption)
		{
			var tcs = new TaskCompletionSource<bool>();

			UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
			{
				if (CheckIfiOS8())
				{
					UIAlertController alertViewController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
					UIAlertAction cancelAction = UIAlertAction.Create(cancelOption, UIAlertActionStyle.Cancel, a => tcs.SetResult(false));
					alertViewController.AddAction(cancelAction);

					UIAlertAction yesAction = UIAlertAction.Create(yesOption, UIAlertActionStyle.Default, a => tcs.SetResult(true));
					alertViewController.AddAction(yesAction);

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alertViewController, true, null);
				}
				else
				{

					UIAlertView alert = new UIAlertView(title, message, null, cancelOption, yesOption);
					alert.Clicked += (sender, buttonArgs) => tcs.TrySetResult(buttonArgs.ButtonIndex != alert.CancelButtonIndex);
					alert.Show();
				}
			}));

			return tcs.Task;
		}

		public Task<int> ShowOptions(string title, string message, MessageboxShowType showType, params MessageboxOption[] options)
		{
			var tcs = new TaskCompletionSource<int>();

			var topVC = ((AppDelegate)UIApplication.SharedApplication.Delegate).Presenter.GetTopMostViewController();

			UIAlertController alertViewController = null;
			if (showType == MessageboxShowType.Center)
			{
				alertViewController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			}
			else
			{
				alertViewController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			}

			if (options != null)
			{
				for (int i = 0; i < options.Length; i++)
				{
					var returnIdx = i;
					UIAlertAction action;
					switch (options[i].Type)
					{
						case MessageboxOptionType.Cancel:
							action = UIAlertAction.Create(options[i].Text, UIAlertActionStyle.Cancel, (obj) => tcs.TrySetResult(returnIdx));
							break;
						case MessageboxOptionType.Default:
							action = UIAlertAction.Create(options[i].Text, UIAlertActionStyle.Default, (obj) => tcs.TrySetResult(returnIdx));
							break;
						case MessageboxOptionType.Destruction:
							action = UIAlertAction.Create(options[i].Text, UIAlertActionStyle.Destructive, (obj) => tcs.TrySetResult(returnIdx));
							break;
						default:
							action = UIAlertAction.Create(options[i].Text, UIAlertActionStyle.Default, (obj) => tcs.TrySetResult(returnIdx));
							break;
					}
					alertViewController.AddAction(action);
				}
			}

			topVC.InvokeOnMainThread(() =>
			{
				topVC.PresentViewController(alertViewController, true, null);
			});

			return tcs.Task;
		}

		public void ShowToastError(string title, string message)
		{
			MessageBarManager.SharedInstance.ShowMessage(title, message, MessageType.Error);
		}

		public void ShowToastScucess(string title, string message)
		{
			MessageBarManager.SharedInstance.ShowMessage(title, message, MessageType.Success);
		}
	}
}
