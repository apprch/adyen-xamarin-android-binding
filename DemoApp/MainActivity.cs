using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Adyen.Checkout.Core;
using Com.Adyen.Checkout.Core.Handler;
using Com.Adyen.Checkout.Core.Model;
using Com.Adyen.Checkout.UI;

namespace DemoApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public partial class MainActivity : AppCompatActivity
	{
		FloatingActionButton fab;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += FabOnClick;
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_settings)
			{
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		public const int RequestCodeCheckout = 17; //Very random

		private void FabOnClick(object sender, EventArgs eventArgs)
		{
			View view = (View)sender;

			Task.Run(async () =>
			{
				var checkoutHandler = new CheckoutSetupParametersHandler
				{
					Error = ex =>
					{
						Android.Util.Log.Error("CheckoutSetupParametersHandler", ex, ex.Message);
					}
				};
				CheckoutController.StartPayment(this, checkoutHandler);
				var encodedPaymentSession = await checkoutHandler.PaymentSessionTask;

				var startPaymentHandler = new StartPaymentParametersHandler
				{
					Error = ex =>
					{
						Android.Util.Log.Error("StartPaymentParametersHandler", ex, ex.Message);
					},
					PaymentInitialized = parameters =>
					{
						var paymentMethodHandler = CheckoutController.GetCheckoutHandler(parameters);
						paymentMethodHandler.HandlePaymentMethodDetails(this, RequestCodeCheckout);
					}
				};
				//This needs to run on the UI thread!
				RunOnUiThread(() => CheckoutController.HandlePaymentSessionResponse(this, encodedPaymentSession, startPaymentHandler));
			});
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (requestCode == RequestCodeCheckout)
			{
				if (resultCode == Result.Ok)
				{
					var paymentResult = PaymentMethodHandlerUtil.GetPaymentResult(data);

					if (paymentResult.ResultCode == PaymentResultCode.Authorized)
					{
						//TODO: Verify result
						RunOnUiThread(() => Snackbar.Make(fab, new Java.Lang.String($"Payment successful: {paymentResult.ResultCode}"), Snackbar.LengthLong).Show());
					}
					else
						RunOnUiThread(() => Snackbar.Make(fab, new Java.Lang.String($"Payment NOT successful: {paymentResult.ResultCode}"), Snackbar.LengthLong).Show());
				}
				else
				{
					var checkoutException = PaymentMethodHandlerUtil.GetCheckoutException(data);
					if (resultCode == Result.Canceled)
					{
						//Handle cancellation and optional checkout exception
						RunOnUiThread(() => Snackbar.Make(fab, new Java.Lang.String($"Payment cancelled: {checkoutException?.Message}"), Snackbar.LengthLong).Show());
					}
					else
					{
						//Handle checkout exception
						RunOnUiThread(() => Snackbar.Make(fab, new Java.Lang.String($"Checkout exception: {checkoutException?.Message}"), Snackbar.LengthLong).Show());
					}
				}
			}
		}
	}
}

