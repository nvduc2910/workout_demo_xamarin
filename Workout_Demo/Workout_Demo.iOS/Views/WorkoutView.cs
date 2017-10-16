using System;
using WorkoutDemo.Core.ViewModels;
using WorkoutDemo.iOS.Views.TableSources;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace WorkoutDemo.iOS.Views
{
    public partial class WorkoutView : BaseView
    {
        public WorkoutView() : base("WorkoutView", null)
        {
        }

		#region ViewModel

        public new WorkoutViewModel ViewModel
		{
			get
			{
				return base.ViewModel as WorkoutViewModel;
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

			var set = this.CreateBindingSet<WorkoutView, WorkoutViewModel>();

			set.Bind(btnBack).To(vm => vm.GoBackCommand);


            var timelines = new WorkoutTableSource(tbWorkout);
			
            set.Bind(timelines).For(s => s.ItemsSource).To(vm => vm.Workout);
            tbWorkout.Source = timelines;

			set.Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

