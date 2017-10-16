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
	[Register ("WorkoutView")]
	partial class WorkoutView
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UITableView tbWorkout { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbWorkout != null) {
				tbWorkout.Dispose ();
				tbWorkout = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}
		}
	}
}
