using System;
using Foundation;
using WorkoutDemo.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace WorkoutDemo.iOS.Views
{
    public partial class RegisterAccountView : BaseView
    {
        public RegisterAccountView() : base("RegisterAccountView", null)
        {
        }

		#region ViewModel

		public new RegisterAccountViewModel ViewModel
		{
			get
			{
				return base.ViewModel as RegisterAccountViewModel;
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

			var set = this.CreateBindingSet<RegisterAccountView, RegisterAccountViewModel>();

            set.Bind(btnLogin).To(vm => vm.LoginCommand);

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

            btnAddAvatar.Layer.CornerRadius = 30 / 2;
            btnAddAvatar.Layer.MasksToBounds = true;

			vAvatar.Layer.CornerRadius = 130 / 2;
			vAvatar.Layer.MasksToBounds = true;

			btnNext.Layer.CornerRadius = 8f;
			btnNext.Layer.MasksToBounds = true;

            vFacebook.Layer.CornerRadius = 8f;
			vFacebook.Layer.MasksToBounds = true;

            tfEmail.AttributedPlaceholder = new NSAttributedString(

				"Email",
				foregroundColor: UIColor.White
			);

            tfPassword.AttributedPlaceholder = new NSAttributedString(

				"Password",
				foregroundColor: UIColor.White
			);

            tfName.AttributedPlaceholder = new NSAttributedString(

				"Name",
				foregroundColor: UIColor.White
			);


		}


		#endregion
	}
}

