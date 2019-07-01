using System;
using Com.Adyen.Checkout.Core;
using Com.Adyen.Checkout.Core.Handler;

namespace DemoApp
{
	public class StartPaymentParametersHandler : Java.Lang.Object, IStartPaymentParametersHandler
	{
		public Action<CheckoutException> Error;
		public Action<IStartPaymentParameters> PaymentInitialized;

		public void OnError(CheckoutException ex) => Error?.Invoke(ex);

		public void OnPaymentInitialized(IStartPaymentParameters parameters) => PaymentInitialized?.Invoke(parameters);
	}
}

