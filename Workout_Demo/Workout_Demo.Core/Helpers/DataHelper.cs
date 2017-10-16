using System;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace WorkoutDemo.Core
{
public static class DataHelper
{
	// key, value
	#region Save, Retrieve
	public static void SaveToUserPref<T>(T value, string key = null)
	{
		var platformService = Mvx.Resolve<IPlatformService>();

		if (string.IsNullOrEmpty(key))
		{
			key = typeof(T).FullName;
		}

		if (value == null)
		{
			platformService.SetPreference(key, "");
			return;
		}

		try
		{
			var valueSer = JsonConvert.SerializeObject(value);
			platformService.SetPreference(key, valueSer);
		}
		catch (Exception ex)
		{
			var errorMsg = ex.Message;
		}
	}

	public static T RetrieveFromUserPref<T>(string key = null)
	{
		if (string.IsNullOrEmpty(key))
		{
			key = typeof(T).FullName;
		}

		try
		{
			var platformService = Mvx.Resolve<IPlatformService>();
			var valueSer = platformService.GetPreference(key);
			return JsonConvert.DeserializeObject<T>(valueSer);
		}
		catch (Exception ex)
		{
			var errorMsg = ex.Message;
			return default(T);
		}
	}
	#endregion	
	}
}