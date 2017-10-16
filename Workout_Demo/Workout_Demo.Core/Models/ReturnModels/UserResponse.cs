using System;
using Newtonsoft.Json;

namespace WorkoutDemo.Core
{
	public class UserResponse
	{
		[JsonProperty("access_token")]
		public string BearerToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty(".issued")]
		public DateTimeOffset Issued { get; set; }

		[JsonProperty(".expires")]
		public DateTimeOffset Expires { get; set; }
	}
}
