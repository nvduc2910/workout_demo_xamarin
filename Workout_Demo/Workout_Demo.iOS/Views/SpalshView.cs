using System;
using WorkoutDemo.Core.ViewModels;
using UIKit;

namespace WorkoutDemo.iOS
{
	public partial class SpalshView : BaseView
	{
		public SpalshView() : base("SpalshView", null)
		{

		}

		#region ViewModel

		public new SplashViewModel ViewModel
		{
			get
			{
				return base.ViewModel as SplashViewModel;
			}
			set
			{
				base.ViewModel = value;
			}
		}

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

