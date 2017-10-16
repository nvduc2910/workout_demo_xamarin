using System;
using WorkoutDemo.Core.Models.ReturnModels;
using MvvmCross.Core.ViewModels;

namespace WorkoutDemo.Core.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public WorkoutViewModel()
        {
            InitData();
        }


		#region Events

        private MvxObservableCollection<Workout> mWorkout = new MvxObservableCollection<Workout>();

		public MvxObservableCollection<Workout> Workout

		{
			get
			{
				return mWorkout;
			}
			set
			{
				mWorkout = value;
				RaisePropertyChanged();
			}
		}
		#endregion

        public void InitData()
        {
            Workout.Add(new Models.ReturnModels.Workout()
            {
                Name = "TRAINGLE POST",
                Description = "Hip and bla bla"
            });

			Workout.Add(new Models.ReturnModels.Workout()
			{
				Name = "POSE 1",
				Description = "Hip and bla bla"
			});

			Workout.Add(new Models.ReturnModels.Workout()
			{
				Name = "POSE 2",
				Description = "Hip and bla bla"
			});

			Workout.Add(new Models.ReturnModels.Workout()
			{
				Name = "JUST A DEMO",
				Description = "Tthis is demo"
			});
        }
	}
}
