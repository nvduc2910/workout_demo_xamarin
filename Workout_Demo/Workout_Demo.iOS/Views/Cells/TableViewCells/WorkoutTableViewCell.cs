using System;

using Foundation;
using WorkoutDemo.Core.Models.ReturnModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace WorkoutDemo.iOS.Views.Cells.TableViewCells
{
    public partial class WorkoutTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("WorkoutTableViewCell");
        public static readonly UINib Nib;


        public UIView VAvatar
        {
            get
            {
                return vAvatar;
            }
        }

        public UIButton BtnTick
        {
            get
            {
                return btnTick;
            }
        }

		public Workout Model { get; set; }

        static WorkoutTableViewCell()
        {
            Nib = UINib.FromName("WorkoutTableViewCell", NSBundle.MainBundle);

			
        }

        protected WorkoutTableViewCell(IntPtr handle) : base(handle)
        {
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<WorkoutTableViewCell, Workout>();

                set.Bind(lbName).To(vm => vm.Name);
                set.Bind(lbDescription).To(vm => vm.Description);
		

				set.Apply();

			});
        }

		public static WorkoutTableViewCell Create()
		{
			return (WorkoutTableViewCell)Nib.Instantiate(null, null)[0];
		}
    }
}
