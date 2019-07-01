using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
	public class DemoBackend
	{
		private const string DemoServerUrl = "https://checkoutshopper-test.adyen.com/checkoutshopper/demoserver/";
		//Insert your demo key here, docs at https://docs.adyen.com/user-management/how-to-get-the-api-key/
		private const string Key = "";

		//These can be changed to your liking
		private const string countryCode = "sv";
		private const string shopperLocale = "en_GB"; //TODO: Use the actual locale of the system
		private const string currency = "EUR";

		private static readonly Uri BaseUri = new Uri(DemoServerUrl);

		public static async Task<string> StartPaymentSession(string token, string returnUrl, decimal amount = 10)
		{
			using (var client = CreateHttpClient())
			{
				var response = await client.PostJsonAsync($"{BaseUri.AbsolutePath}paymentSession", new
				{
					countryCode,
					shopperLocale,
					reference = Guid.NewGuid().ToString(), //We just make this up!
					amount = new
					{
						currency,
						value = amount * 100 //I think we need to multiply by 100
					},
					token,
					returnUrl
				});

				return await response.Content.ReadAsStringAsync();
			}
		}

		public static async Task<PaymentResult> GetPaymentResult(string payload)
		{
			using (var client = CreateHttpClient())
			{
				var response = await client.PostJsonAsync($"{BaseUri.AbsolutePath}payments/result", new { payload });

				return await response.Content.DeserializeJsonAsync<PaymentResult>();
			}
		}

		private static HttpClient CreateHttpClient()
		{
			var client = new HttpClient
			{
				BaseAddress = BaseUri
			};

			client.DefaultRequestHeaders.Add("x-demo-server-api-key", Key);

			return client;
		}
	}

	public class PaymentResult
	{
		public string ResultCode { get; set; }
		public string PspReference { get; set; }
		public string MerchantReference { get; set; }
		public string PaymentMethod { get; set; }
		public string ShopperLocale { get; set; }
	}

	public static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, object data)
		{
			var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
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
			catch
			{
				return null;
			}
		}
	}
}

