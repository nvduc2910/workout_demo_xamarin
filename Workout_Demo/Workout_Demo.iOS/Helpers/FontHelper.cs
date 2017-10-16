using System;
using UIKit;

namespace WorkoutDemo.iOS
{
	public class FontHelper
	{
		private static readonly float widthRatio = (float)UIScreen.MainScreen.Bounds.Width / 375.0f;

		public static UIFont AdjustFontSize(UIFont font)
		{
			return font.WithSize(font.PointSize * widthRatio);
		}

		public static nfloat GetAdjustFontSize(UIFont font)
		{
			return font.PointSize * widthRatio;
		}
	}
}
