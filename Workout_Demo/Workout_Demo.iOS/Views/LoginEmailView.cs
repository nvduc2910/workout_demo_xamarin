using System;
using Foundation;
using WorkoutDemo.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace WorkoutDemo.iOS.Views
{
    public partial class LoginEmailView : BaseView
    {
        public LoginEmailView() : base("LoginEmailView", null)
        {
        }


		#region ViewModel

		public new LoginEmailViewModel ViewModel
		{
			get
			{
				return base.ViewModel as LoginEmailViewModel;
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

            var set = this.CreateBindingSet<LoginEmailView, LoginEmailViewModel>();

            set.Bind(btnNext).To(vm => vm.NextCommand);
            set.Bind(btnSignup).To(vm => vm.SignUpCommand);

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
            btnNext.Layer.CornerRadius = 8f;
            btnNext.Layer.MasksToBounds = true;

            vLoginFacebook.Layer.CornerRadius = 8f;
            vLoginFacebook.Layer.MasksToBounds = true;

			tfEmail.AttributedPlaceholder = new NSAttributedString(
                
				"Email",
                foregroundColor: UIColor.White
            );
        }


        #endregion
    }
}

