using WorkoutDemo.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace WorkoutDemo.Core
{
	public class App : MvxApplication
	{
		public App()
		{
            RegisterAppStart<LoginEmailViewModel>();
		}
	}
}
