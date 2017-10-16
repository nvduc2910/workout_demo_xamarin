using System;
using MvvmCross.Core.ViewModels;

namespace WorkoutDemo.Core.ViewModels
{
    public class LoginPasswordViewModel : BaseViewModel
    {
        public LoginPasswordViewModel()
        {

        }



        #region LoginCommand
        private MvxCommand mLoginCommand;

        public MvxCommand LoginCommand
        {
            get
            {
                if (mLoginCommand == null)
                {
                    mLoginCommand = new MvxCommand(this.Login);
                }
                return mLoginCommand;
            }
        }

        private void Login()
        {
            ShowViewModel<WorkoutViewModel>();
        }
        #endregion

    }
}
