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
					Config.CountryCode,
					Config.MerchantAccount,
					amount = new
					{
						Config.Currency,
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
				Android.Util.Log.Error("DemoBackend", $"{errorData.ErrorMessage} ({errorData.ErrorCode})");

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
					Config.CountryCode,
					Config.ShopperLocale,
					Config.MerchantAccount,
					reference = Guid.NewGuid().ToString(), //We just make this up!
					amount = new
					{
						Config.Currency,
						value = amount * 100 //I think we need to multiply by 100
					},
					returnUrl,
					paymentMethod = new
					{
						paymentMethod.Type,
						paymentMethod.EncryptedCardNumber,
						paymentMethod.EncryptedExpiryMonth,
						paymentMethod.EncryptedExpiryYear,
						paymentMethod.EncryptedSecurityCode
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
					Android.Util.Log.Error("DemoBackend", $"{errorData.ErrorMessage} ({errorData.ErrorCode})");
					return new PaymentsResponse
					{
						ResultCode = "Error",
						RefusalReason = errorData.ErrorMessage,
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
			ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
		};


		public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, object data)
		{
			var content = new StringContent(JsonConvert.SerializeObject(data, settings), Encoding.UTF8, "application/json");
			return client.PostAsync(requestUri, content);
		}

		public static async Task<T> DeserializeJsonAsync<T>(this HttpContent content) where T : class, new()
		{
			if (content.Headers.ContentType.MediaType != "application/json")
				throw new InvalidOperationException($"Content type is {content.Headers.ContentType.MediaType}, expected appliation/json!");

			var str = await content.ReadAsStringAsync();

			if (string.IsNullOrWhiteSpace(str))
				throw new InvalidOperationException($"Content was empty!");

			try
			{
				var data = JsonConvert.DeserializeObject<T>(str);
				return data;
			}
			catch (Exception ex)
			{
				Android.Util.Log.Error("HttpClientExtensions", Java.Lang.Throwable.FromException(ex), "Could not deserialize content");
				return null;
			}
		}
	}
}

