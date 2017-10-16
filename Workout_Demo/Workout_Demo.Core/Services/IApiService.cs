using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace WorkoutDemo.Core
{
	public interface IApiService
	{

	}


	public class APIResponse<T> where T : class
	{
		public System.Net.HttpStatusCode StatusCode;
		public T Data;
		public string ErrorData;

		public APIResponse(T data, HttpStatusCode statusCode, string errorData = null)
		{
			this.StatusCode = statusCode;
			this.Data = data;
			this.ErrorData = errorData;
		}
	}
	public class ErrorResponse
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("error_description")]
		public string Description { get; set; }

	}


	public static class ApiClient
	{

		public static HttpClient Client()
		{
			var handler = new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};

			var httpClient = new HttpClient(handler);
			httpClient.Timeout = TimeSpan.FromSeconds(60);
			return httpClient;
		}

		public static Task<HttpResponseMessage> SendPostAsync(this HttpClient client,
															  string url,
															  string token,
															  object data)
		{

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url.ToString());
			request.Headers.Add("Authorization", "Bearer " + token);
			request.Headers.Add("Accept", "application/json");

			var jsonPost = JsonConvert.SerializeObject(data);
			request.Content = new StringContent(jsonPost, System.Text.Encoding.UTF8, "application/json");
			return client.SendAsync(request);
		}

		public static Task<HttpResponseMessage> SendGetAsync(this HttpClient client,
															 string url,
															 string token)
		{

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url.ToString());
			request.Headers.Add("Authorization", "Bearer " + token);
			request.Headers.Add("Accept", "application/json");

			return client.SendAsync(request);
		}


		public static bool TryGetResponse<T>(this HttpResponseMessage response, out APIResponse<T> result)
		 where T : class
		{
			try
			{
				var data = response.Content.ReadAsStringAsync().Result;
				if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
				{
					T objecReturn = JsonConvert.DeserializeObject<T>(data);
					result = new APIResponse<T>(objecReturn, HttpStatusCode.OK);
					return true;
				}
			}
			catch (Exception)
			{
			}

			result = new APIResponse<T>(null, response.StatusCode);
			return false;
		}
	}


	public class ApiService : BaseViewModel, IApiService
	{
		public ApiService()
		{

		}

		#region UseBeaerToken

		public bool UseBeaerToken { get; set; }

		#endregion

		#region BeaerToken

		private string mBeaerToken
		{
			get
			{
				if (UseBeaerToken && mUserData != null)
				{
					return mUserData.BearerToken;
				}
				return null;
			}
		}

		#endregion

		#region mUserData

		private UserResponse mUserData
		{
			get
			{
				return Mvx.Resolve<ICacheService>().CurrentUserData;
			}
			set
			{
				Mvx.Resolve<ICacheService>().CurrentUserData = value;
			}
		}

		#endregion

		#region Base Handle


		#region CheckInternet

		private static bool CheckInternet()
		{
			if (!Mvx.Resolve<IPlatformService>().DetectInternerConnection())
			{
				Mvx.Resolve<IMessageboxService>().ShowToast("Cannot connect internet.\nPlease recheck your connection.");
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion

		#region CheckBearerToken

		async Task<bool> CheckBearerToken()
		{
			if (!UseBeaerToken)
				return true;

			if (mUserData.Expires < DateTimeOffset.Now)
			{
				try
				{
                    ClearStackAndShowViewModel<LoginEmailViewModel>();
				}
				catch (FlurlHttpTimeoutException)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return false;
				}
				catch (FlurlHttpException ex)
				{
#if DEBUG
					if (ex.Call.Response != null)
						Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
					else
						Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return false;
				}
				catch (Exception ex)
				{
					Mvx.Resolve<IMessageboxService>().ShowToast(ex.Message);
					return false;
				}
			}

			return true;
		}

		#endregion

		#region HandleForGET

		async Task<APIResponse<T>> HandleForGET<T>(Url url, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			try
			{
				var urlString = url.ToString();
				var response = await ApiClient.Client().SendGetAsync(urlString, mBeaerToken);

				var result = default(APIResponse<T>);
				response.TryGetResponse<T>(out result);
				return result;
			}
			catch (Exception exc)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}
			}

			return new APIResponse<T>(null, (HttpStatusCode)1000);
		}

		#endregion

		#region HandleForPOST

		async Task<APIResponse<T>> HandleForPOST<T>(Url url, object jsonObject = null, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);
			}

			var jsonString = JsonConvert.SerializeObject(jsonObject);

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			try
			{
				var response = await ApiClient.Client().SendPostAsync(url.ToString(), mBeaerToken, jsonObject);

				var result = default(APIResponse<T>);
				response.TryGetResponse(out result);
				return result;
			}
			catch (Exception exc)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}
			}
			return null;
		}

		#endregion

		#region HandleForPUT

		async Task<APIResponse<T>> HandleForPUT<T>(Url url, object jsonObject = null, bool needShowError = true) where T : class
		{
			if (!CheckInternet())
			{
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable); ;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return new APIResponse<T>(null, HttpStatusCode.ServiceUnavailable);

			try
			{
				string data;
				HttpResponseMessage response;

				if (string.IsNullOrEmpty(mBeaerToken))
				{
					response = await url.WithTimeout(60).PutJsonAsync(jsonObject);
				}
				else
				{
					response = await url.WithOAuthBearerToken(mBeaerToken).WithTimeout(60).PutJsonAsync(jsonObject);
				}

				data = await response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(data))
				{
					T objecReturn = JsonConvert.DeserializeObject<T>(data);
					return new APIResponse<T>(objecReturn, HttpStatusCode.OK);
				}

				return new APIResponse<T>(null, response.StatusCode);
			}
			catch (FlurlHttpTimeoutException)
			{
				if (needShowError)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}

				return new APIResponse<T>(null, HttpStatusCode.RequestTimeout);
			}
			catch (FlurlHttpException ex)
			{
				if (ex.Call.Response != null)
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
					return new APIResponse<T>(null, ex.Call.Response.StatusCode, ex.Call.ErrorResponseBody);
				}
				else
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}

				return new APIResponse<T>(null, HttpStatusCode.MethodNotAllowed);
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			return null;
		}

		async Task<bool> HandleForPUT(Url url, object jsonObject = null)
		{
			if (!CheckInternet())
			{
				return false;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return false;

			try
			{
				if (string.IsNullOrEmpty(mBeaerToken))
				{
					await url.WithTimeout(60).PutJsonAsync(jsonObject).ReceiveString();
				}
				else
				{
					await url.WithOAuthBearerToken(mBeaerToken).WithTimeout(60).PutJsonAsync(jsonObject).ReceiveString();
				}

				return true;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (FlurlHttpException ex)
			{
#if DEBUG
				if (ex.Call.Response != null)
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
				else
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			return false;
		}

		#endregion

		#region HandleForDELETE

		async Task<bool> HandleForDELETE(Url url)
		{
			if (!CheckInternet())
			{
				return false;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return false;

			try
			{
				if (string.IsNullOrEmpty(mBeaerToken))
				{
					await url.WithTimeout(60).DeleteAsync();
				}
				else
				{
					await url.WithOAuthBearerToken(mBeaerToken).WithTimeout(60).DeleteAsync();
				}

				return true;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (FlurlHttpException ex)
			{
#if DEBUG
				if (ex.Call.Response != null)
					Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
				else
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}

			return false;
		}

		async Task<HttpStatusCode> HandleStatusCodeForDELETE(Url url)
		{
			if (!CheckInternet())
			{
				return HttpStatusCode.ServiceUnavailable;
			}

			bool succeed = await CheckBearerToken();
			if (!succeed)
				return HttpStatusCode.ServiceUnavailable;

			try
			{
				HttpResponseMessage message = await url.DeleteAsync();

				return message.StatusCode;
			}
			catch (FlurlHttpTimeoutException)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast("Timed out!");
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				return HttpStatusCode.RequestTimeout;
			}
			catch (FlurlHttpException ex)
			{
				if (ex.Call.Response != null)
				{
					//Mvx.Resolve<IMessageboxService>().ShowToast("Failed with response code " + ex.Call.Response.StatusCode);
					return ex.Call.Response.StatusCode;
				}
				else
				{
#if DEBUG
					Mvx.Resolve<IMessageboxService>().ShowToast("Totally failed before getting a response! " + ex.Message);
#else

			Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
				}
			}
			catch (Exception exc)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(exc.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif
			}

			return (HttpStatusCode)1000;
		}

		#endregion

		#region Login

		public async Task<APIResponse<UserResponse>> PostLogin(string email, string password)
		{
			try
			{
				var url = ApiUrls.ApiToken;
				HttpResponseMessage response;

				var body = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("username", email),
					new KeyValuePair<string, string>("password", password),
					new KeyValuePair<string, string>("grant_type", "password"),
				};

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url.ToString());
				request.Headers.Add("Accept", "application/json");

				request.Content = new FormUrlEncodedContent(body); ;
				response = await ApiClient.Client().SendAsync(request);

				var jsonData = await response.Content.ReadAsStringAsync();

				APIResponse<UserResponse> result;
				response.TryGetResponse(out result);

				if (result.Data == null)
				{
					return new APIResponse<UserResponse>(null, response.StatusCode, jsonData);
				}
				return result;
			}
			catch (Exception ex)
			{
#if DEBUG
				Mvx.Resolve<IMessageboxService>().ShowToast(ex.Message);
#else
		Mvx.Resolve<IMessageboxService>().ShowToast("Oops!");
#endif

				return new APIResponse<UserResponse>(null, HttpStatusCode.MethodNotAllowed);
			}
		}

		#endregion

		#endregion
	}
}
