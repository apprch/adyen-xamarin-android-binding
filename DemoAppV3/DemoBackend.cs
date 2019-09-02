using Com.Adyen.Checkout.Base.Model.Payments.Request;
using DemoAppV3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoAppV3
{
	public class DemoBackend
	{
		private static readonly Uri BaseUri = new Uri(Config.ServerUrl);

		public static async Task<IReadOnlyList<Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod>> GetPaymentMethods(decimal amount = 10)
		{
			using (var client = CreateHttpClient())
			{
				var response = await client.PostJsonAsync($"{BaseUri.AbsolutePath}paymentMethods", new
				{
					countryCode = Config.CountryCode,
					merchantAccount = Config.MerchantAccount,
					amount = new
					{
						currency = Config.Currency,
						value = amount * 100 //I think we need to multiply by 100
					},
					channel = "Android"
				});

				if (response.StatusCode == HttpStatusCode.OK)
				{
					var getPaymentMethodsResponse = await response.Content.DeserializeJsonAsync<GetPaymentMethodsResponse>();
					var methods = ParsePaymentMethodResponse(getPaymentMethodsResponse);
					return methods ?? new List<Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod>();
				}

				var errorData = await response.Content.DeserializeJsonAsync<ErrorResponse>();
				Android.Util.Log.Error("DemoBackend", $"{errorData.Message} ({errorData.ErrorCode})");

				return new List<Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod>();
			}
		}

		private static List<Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod> ParsePaymentMethodResponse(GetPaymentMethodsResponse response)
		{
			return response?.PaymentMethods?.Select(x => new Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod
			{
				Name = x.Name,
				Type = x.Type,
				SupportsRecurring = x.SupportsRecurring
			}).ToList() ?? new List<Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod>();
		}


		public static async Task<PaymentsResponse> ExecutePayment(CardPaymentMethod paymentMethod, string returnUrl, decimal amount = 10)
		{
			using (var client = CreateHttpClient())
			{
				var response = await client.PostJsonAsync($"{BaseUri.AbsolutePath}payments", new
				{
					countryCode = Config.CountryCode,
					shopperLocale = Config.ShopperLocale,
					merchantAccount = Config.MerchantAccount,
					reference = Guid.NewGuid().ToString(), //We just make this up!
					amount = new
					{
						currency = Config.Currency,
						value = amount * 100 //I think we need to multiply by 100
					},
					returnUrl,
					paymentMethod = new
					{
						type = paymentMethod.Type,
						encryptedCardNumber = paymentMethod.EncryptedCardNumber,
						encryptedExpiryMonth = paymentMethod.EncryptedExpiryMonth,
						encryptedExpiryYear = paymentMethod.EncryptedExpiryYear,
						encryptedSecurityCode = paymentMethod.EncryptedSecurityCode
					},
					//This is for 3D Secure
					additionalData = new { executeThreeD = true, allow3DS2 = false },
					merchantRiskIndicator = new { deliveryAddressIndicator = "digitalGoods", deliveryEmail = "test@example.com" },
					shopperEmail = "test@example.com",
					holderName = "Test Testsson",
					channel = "Android"
				});

				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.DeserializeJsonAsync<PaymentsResponse>();
					return responseData;
				}
				else
				{
					var errorData = await response.Content.DeserializeJsonAsync<ErrorResponse>();
					Android.Util.Log.Error("DemoBackend", $"{errorData.Message} ({errorData.ErrorCode})");
					return new PaymentsResponse
					{
						ResultCode = "Error",
						RefusalReason = $"{errorData.Message} ({errorData.ErrorType})",
						RefusalReasonCode = errorData.ErrorCode
					};
				}
			}
		}

		public static async Task<PaymentsResponse> GetPaymentDetails(string paRes, string md, string paymentData)
		{
			using (var client = CreateHttpClient())
			{
				var response = await client.PostJsonAsync($"{BaseUri.AbsolutePath}payments/details", new
				{
					details = new
					{
						PaRes = paRes,
						MD = md
					},
					paymentData
				});

				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.DeserializeJsonAsync<PaymentsResponse>();
					return responseData;
				}
				else
				{
					var errorData = await response.Content.DeserializeJsonAsync<ErrorResponse>();
					Android.Util.Log.Error("DemoBackend", $"{errorData.Message} ({errorData.ErrorCode})");
					return new PaymentsResponse
					{
						ResultCode = "Error",
						RefusalReason = $"{errorData.Message} ({errorData.ErrorType})",
						RefusalReasonCode = errorData.ErrorCode
					};
				}
			}
		}

		private static HttpClient CreateHttpClient()
		{
			var client = new HttpClient
			{
				BaseAddress = BaseUri
			};

			client.DefaultRequestHeaders.Add("X-API-KEY", Config.ApiKey);

			return client;
		}
	}

	public static class HttpClientExtensions
	{
		private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
		{
			ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
			{
				NamingStrategy = new Newtonsoft.Json.Serialization.DefaultNamingStrategy
				{
					OverrideSpecifiedNames = false
				}
			}
		};


		public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, object data)
		{
			var json = JsonConvert.SerializeObject(data, settings);

			//DebugLog($"POST {requestUri}\r\n{json}");

			var content = new StringContent(json, Encoding.UTF8, "application/json");
			return client.PostAsync(requestUri, content);
		}

		public static async Task<T> DeserializeJsonAsync<T>(this HttpContent content) where T : class, new()
		{
			if (content.Headers.ContentType.MediaType != "application/json")
				throw new InvalidOperationException($"Content type is {content.Headers.ContentType.MediaType}, expected appliation/json!");

			var json = await content.ReadAsStringAsync();

			if (string.IsNullOrWhiteSpace(json))
				throw new InvalidOperationException($"Content was empty!");

			//DebugLog($"Response\r\n{json}");

			try
			{
				var data = JsonConvert.DeserializeObject<T>(json);
				return data;
			}
			catch (Exception ex)
			{
				Android.Util.Log.Error("HttpClientExtensions", Java.Lang.Throwable.FromException(ex), "Could not deserialize content");
				return null;
			}
		}

		private static void DebugLog(string str)
		{
			var maxSize = 2000;
			var index = 0;

			while (index < str.Length)
			{
				var s = index + maxSize < str.Length ? str.Substring(index, maxSize) : str.Substring(index);
				index += maxSize;
				Android.Util.Log.Debug("DemoBackend", s);
			};
		}
	}
}

