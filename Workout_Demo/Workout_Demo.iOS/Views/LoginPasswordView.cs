using System;
using Foundation;
using WorkoutDemo.Core;
using WorkoutDemo.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace WorkoutDemo.iOS.Views
{
    public partial class LoginPasswordView : BaseView
    {
        public LoginPasswordView() : base("LoginPasswordView", null)
        {
        }
		#region ViewModel

        public new LoginPasswordViewModel ViewModel
		{
			get
			{
                return base.ViewModel as LoginPasswordViewModel;
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

            InitView();

			var set = this.CreateBindingSet<LoginPasswordView, LoginPasswordViewModel>();

            set.Bind(btnBack).To(vm => vm.GoBackCommand);
            set.Bind(btnNext).To(vm => vm.LoginCommand);
			set.Apply();


			// Perform any additional setup after loading the view, typically from a nib.
		}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


		#region InitView


		private void InitView()
		{
            vAvatar.Layer.CornerRadius = 150 / 2;
            vAvatar.Layer.MasksToBounds = true;
            
			btnNext.Layer.CornerRadius = 8f;
			btnNext.Layer.MasksToBounds = true;

            btnBack.Layer.CornerRadius = 8f;
			btnBack.Layer.MasksToBounds = true;

            tfPassword.AttributedPlaceholder = new NSAttributedString(

				"Email",
				foregroundColor: UIColor.White
			);
		}


		#endregion
	}
}

