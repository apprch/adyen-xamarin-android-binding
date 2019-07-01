using System;
using System.Threading.Tasks;
using Com.Adyen.Checkout.Core;
using Com.Adyen.Checkout.UI;

namespace DemoApp
{
	public class CheckoutSetupParametersHandler : Java.Lang.Object, ICheckoutSetupParametersHandler
	{
		public Action<CheckoutException> Error;
		public Action<ICheckoutSetupParameters> RequestPaymentSession;

		public Task<string> PaymentSessionTask { get; private set; }

		public void OnError(CheckoutException ex) => Error?.Invoke(ex);

		public void OnRequestPaymentSession(ICheckoutSetupParameters parameters)
		{
			PaymentSessionTask = DemoBackend.StartPaymentSession(parameters.SdkToken, parameters.ReturnUrl);
			RequestPaymentSession?.Invoke(parameters);
		}
	}
}

