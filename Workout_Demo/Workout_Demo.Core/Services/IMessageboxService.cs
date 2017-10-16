using System;
using System.Threading.Tasks;

namespace WorkoutDemo.Core
{
	public enum MessageboxOptionType
	{
		Default,
		Cancel,
		Destruction
	}

	public enum MessageboxShowType
	{
		Center = 0,
		Bottom
	}

	public class MessageboxOption
	{
		public string Text { get; set; }
		public MessageboxOptionType Type { get; set; }
	}

	public interface IMessageboxService
	{
		void Show(string title, string message);

		Task<bool> ShowTwoOptions(string title, string message, string cancelOption, string yesOption);

		Task<int> ShowThreeOptions(string title, string message, string cancelOption, string firstOption, string secondOption);

		Task<int> ShowOptions(string title, string message, MessageboxShowType showType, params MessageboxOption[] options);

		void ShowToast(string message, int timeoutMs = 1500);
		void ShowToastError(string title, string message);
		void ShowToastScucess(string title, string message);
	}
}
