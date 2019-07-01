using System;
using System.Linq;
using Android.Runtime;

//These interfaces were missing after the code generation.
//It seems to work fine with just these empty interfaces, but should need arise, the originals can be found at
//https://github.com/Adyen/adyen-android/tree/2.4.5/checkout-core/src/main/java/com/adyen/checkout/core/internal/persistence

namespace Com.Adyen.Checkout.Core.Internal.Persistence
{
	public interface IPaymentSessionDao : IJavaObject
	{

	}

	public interface IPaymentInitiationResponseDao : IJavaObject
	{

	}
}