using WorkoutDemo.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using UIKit;

namespace WorkoutDemo.iOS
{
	public class Setup : MvxIosSetup
	{
		public Setup(MvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter)
			: base(appDelegate, presenter)
		{

		}


		protected override IMvxApplication CreateApp()
		{
			Mvx.RegisterSingleton<IApiService>(new ApiService());
			Mvx.RegisterSingleton<IMessageboxService>(new MessageboxService());
			Mvx.RegisterSingleton<IProgressDialogService>(new ProgressDialogService());
			Mvx.RegisterSingleton<IPlatformService>(new PlatformServices());
			Mvx.RegisterSingleton<ICacheService>(new CacheService());
			Mvx.RegisterSingleton<IImageService>(new ImageService());

			return new App();
		}

		protected override void FillTargetFactories(MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);
		}
	}

	public class CustomPresenter : MvxIosViewPresenter
	{
		public CustomPresenter(UIApplicationDelegate appDelegate, UIWindow window)
			: base(appDelegate, window)
		{

		}

		private IMvxIosViewCreator _viewCreator;
		private UIViewController popupViewController;
		private UIViewController closeViewController;

		protected IMvxIosViewCreator ViewCreator
		{
			get { return _viewCreator ?? (_viewCreator = Mvx.Resolve<IMvxIosViewCreator>()); }
		}

		protected override UIKit.UINavigationController CreateNavigationController(UIKit.UIViewController viewController)
		{
			var toReturn = base.CreateNavigationController(viewController);
			toReturn.SetNavigationBarHidden(true, true);
			return toReturn;
		}

		public UIViewController GetTopMostViewController()
		{
			return MasterNavigationController.TopViewController;
		}

		public override void Show(MvvmCross.Core.ViewModels.MvxViewModelRequest request)
		{
			if (request.PresentationValues != null)
			{
				#region ClearStack
				if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
				{
					//MasterNavigationController.PopToRootViewController(false);
					var nextViewController = (UIViewController)ViewCreator.CreateView(request);
					MasterNavigationController.PushViewController(nextViewController, true);
					return;
				}
				#endregion

				#region ShowBack
				if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ShowBack))
				{

					var nextViewController = (UIViewController)ViewCreator.CreateView(request);
					var currentViewController = MasterNavigationController.TopViewController;
					bool animated = false;

					while (!animated && currentViewController != null && currentViewController.GetType() != nextViewController.GetType())
					{

						if (MasterNavigationController.ViewControllers.Length > 1)
						{
							var backViewController = MasterNavigationController.ViewControllers[MasterNavigationController.ViewControllers.Length - 2];
							if (backViewController.GetType() == nextViewController.GetType())
								animated = true;
						}
						else
						{
							break;
						}

						MasterNavigationController.PopViewController(animated);

						if (animated)
						{
							MemoryUtils.DelayReleaseObject(2000, currentViewController);
						}
						else
						{
							MemoryUtils.ReleaseObject(currentViewController);
						}

						currentViewController = MasterNavigationController.TopViewController;
					}

					if (MasterNavigationController.ViewControllers.Length <= 1)
						MasterNavigationController.PushViewController(nextViewController, true);

					return;

				}
				#endregion

				#region CloseCurrentAndShow
				if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.CloseCurrentAndShow))
				{

					// let make a async task to remove current from history to not make UI flash
					System.Threading.Tasks.Task.Run(async () =>
						{
							await System.Threading.Tasks.Task.Delay(1000);

							if (closeViewController == null)
								return;
							MasterNavigationController.InvokeOnMainThread(async () =>
								{

								//wait for until top view controller is showed
								while (closeViewController != MasterNavigationController.TopViewController)
									{
										await System.Threading.Tasks.Task.Delay(100);
									}

									int countVc = MasterNavigationController.ViewControllers.Length;

									if (countVc > 1)
									{
										UIViewController releaseViewController = MasterNavigationController.ViewControllers[countVc - 2];
										UIViewController[] newHistory = new UIViewController[countVc - 1];
										for (int i = 0; i < countVc - 2; i++)
										{
											newHistory[i] = MasterNavigationController.ViewControllers[i];
										}
										newHistory[countVc - 2] = MasterNavigationController.ViewControllers[countVc - 1];
										MasterNavigationController.ViewControllers = newHistory;

										MemoryUtils.ReleaseObject(releaseViewController);
									}

									closeViewController = null;
								});
						});
				}
				#endregion
			}
			base.Show(request);
		}

		public override void Show(IMvxIosView view)
		{
			#region CloseCurrentAndShow
			if (view.Request.PresentationValues != null && view.Request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.CloseCurrentAndShow))
			{
				closeViewController = (UIViewController)view;
			}
			#endregion

			base.Show(view);
		}
	}
}
