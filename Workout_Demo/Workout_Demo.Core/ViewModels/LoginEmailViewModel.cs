using System;
using WorkoutDemo.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace WorkoutDemo.Core
{
    public class LoginEmailViewModel : BaseViewModel
    {

        #region Init

        public void Init()
        {

        }

        #endregion


        #region Commands

        #region NextCommand
        private MvxCommand mNextCommand;

        public MvxCommand NextCommand
        {
            get
            {
                if (mNextCommand == null)
                {
                    mNextCommand = new MvxCommand(this.Next);
                }
                return mNextCommand;
            }
        }

        private void Next()
        {
            ShowViewModel<LoginPasswordViewModel>();
        }
        #endregion

        #region SignUpCommand
        private MvxCommand mSignUpCommand;

        public MvxCommand SignUpCommand
        {
            get
            {
                if (mSignUpCommand == null)
                {
                    mSignUpCommand = new MvxCommand(this.SignUp);
                }
                return mSignUpCommand;
            }
        }

        private void SignUp()
        {
            ShowViewModel<RegisterAccountViewModel>();
        }
        #endregion



        #endregion
    }
}
