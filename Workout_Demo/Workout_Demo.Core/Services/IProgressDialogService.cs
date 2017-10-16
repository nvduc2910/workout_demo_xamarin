using System;
namespace WorkoutDemo.Core
{
	public interface IProgressDialogService
	{
		void ShowProgressDialog(string message = "");

		void HideProgressDialog();
	}
}
