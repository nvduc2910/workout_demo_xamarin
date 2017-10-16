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
	[Register ("LoginEmailView")]
	partial class LoginEmailView
	{
		[Outlet]
		UIKit.UIButton btnLoginFacebook { get; set; }

		[Outlet]
		UIKit.UIButton btnNext { get; set; }

		[Outlet]
		UIKit.UIButton btnSignup { get; set; }

		[Outlet]
		UIKit.UITextField tfEmail { get; set; }

		[Outlet]
		UIKit.UIView vLoginFacebook { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tfEmail != null) {
				tfEmail.Dispose ();
				tfEmail = null;
			}

			if (btnNext != null) {
				btnNext.Dispose ();
				btnNext = null;
			}

			if (btnLoginFacebook != null) {
				btnLoginFacebook.Dispose ();
				btnLoginFacebook = null;
			}

			if (btnSignup != null) {
				btnSignup.Dispose ();
				btnSignup = null;
			}

			if (vLoginFacebook != null) {
				vLoginFacebook.Dispose ();
				vLoginFacebook = null;
			}
		}
	}
}
