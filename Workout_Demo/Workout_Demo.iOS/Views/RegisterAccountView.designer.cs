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
	[Register ("RegisterAccountView")]
	partial class RegisterAccountView
	{
		[Outlet]
		UIKit.UIButton btnAddAvatar { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		UIKit.UIButton btnNext { get; set; }

		[Outlet]
		UIKit.UITextField tfEmail { get; set; }

		[Outlet]
		UIKit.UITextField tfName { get; set; }

		[Outlet]
		UIKit.UITextField tfPassword { get; set; }

		[Outlet]
		UIKit.UIView vAvatar { get; set; }

		[Outlet]
		UIKit.UIView vFacebook { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vAvatar != null) {
				vAvatar.Dispose ();
				vAvatar = null;
			}

			if (tfEmail != null) {
				tfEmail.Dispose ();
				tfEmail = null;
			}

			if (tfPassword != null) {
				tfPassword.Dispose ();
				tfPassword = null;
			}

			if (tfName != null) {
				tfName.Dispose ();
				tfName = null;
			}

			if (btnNext != null) {
				btnNext.Dispose ();
				btnNext = null;
			}

			if (vFacebook != null) {
				vFacebook.Dispose ();
				vFacebook = null;
			}

			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (btnAddAvatar != null) {
				btnAddAvatar.Dispose ();
				btnAddAvatar = null;
			}
		}
	}
}
