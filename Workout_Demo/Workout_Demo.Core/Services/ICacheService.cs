using System;
namespace WorkoutDemo.Core
{
	public interface ICacheService
	{
		UserResponse CurrentUserData { get; set; }
	}
	public class CacheService : ICacheService
	{
		#region CurrentUserData
		private UserResponse mCurrentUserData;
		public UserResponse CurrentUserData
		{
			get
			{
				if (mCurrentUserData == null)
					mCurrentUserData = DataHelper.RetrieveFromUserPref<UserResponse>();
				return mCurrentUserData;
			}
			set
			{
				mCurrentUserData = value;
				DataHelper.SaveToUserPref<UserResponse>(mCurrentUserData);
			}
		}

		#endregion
	}
}
