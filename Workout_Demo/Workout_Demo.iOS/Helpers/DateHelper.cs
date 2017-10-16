using System;
using Foundation;

namespace WorkoutDemo.iOS
{
public static class DateHelper
{
	public static DateTime NSDateToDateTime(this NSDate date)
	{
		DateTime reference =
			new DateTime(2001, 1, 1, 0, 0, 0);
		DateTime dt = reference.AddSeconds(date.SecondsSinceReferenceDate).ToLocalTime();
		return dt;
	}

	public static NSDate DateTimeToNSDate(this DateTime date)
	{
		//          DateTime reference = 
		//              new DateTime (2001, 1, 1, 0, 0, 0);
		//          return NSDate.FromTimeIntervalSinceReferenceDate (
		//              (date - reference).TotalSeconds);

		//          DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.MinValue);
		//          return NSDate.FromTimeIntervalSinceReferenceDate((date - reference).TotalSeconds);

		if (date.Kind == DateTimeKind.Unspecified)
			date = DateTime.SpecifyKind(date, DateTimeKind.Local);
		return (NSDate)date;
	}	}
}
