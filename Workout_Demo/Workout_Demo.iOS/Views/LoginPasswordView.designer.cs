// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WorkoutDemo.iOS.Views
{
	[Register ("LoginPasswordView")]
	partial class LoginPasswordView
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnForgotpassword { get; set; }

		[Outlet]
		UIKit.UIButton btnNext { get; set; }

		[Outlet]
		UIKit.UITextField tfPassword { get; set; }

		[Outlet]
		UIKit.UIView vAvatar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vAvatar != null) {
				vAvatar.Dispose ();
				vAvatar = null;
			}

			if (tfPassword != null) {
				tfPassword.Dispose ();
				tfPassword = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnNext != null) {
				btnNext.Dispose ();
				btnNext = null;
			}

			if (btnForgotpassword != null) {
				btnForgotpassword.Dispose ();
				btnForgotpassword = null;
			}
		}
	}
}
