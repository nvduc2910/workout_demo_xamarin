// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WorkoutDemo.iOS.Views.Cells.TableViewCells
{
	[Register ("WorkoutTableViewCell")]
	partial class WorkoutTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnTick { get; set; }

		[Outlet]
		UIKit.UILabel lbDescription { get; set; }

		[Outlet]
		UIKit.UILabel lbName { get; set; }

		[Outlet]
		UIKit.UIView vAvatar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (vAvatar != null) {
				vAvatar.Dispose ();
				vAvatar = null;
			}

			if (lbName != null) {
				lbName.Dispose ();
				lbName = null;
			}

			if (lbDescription != null) {
				lbDescription.Dispose ();
				lbDescription = null;
			}

			if (btnTick != null) {
				btnTick.Dispose ();
				btnTick = null;
			}
		}
	}
}
