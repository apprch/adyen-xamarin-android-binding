using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Adyen.Checkout.Base.Model.Payments.Request;
using Com.Adyen.Checkout.Card;
using Java.Lang;

namespace DemoAppV3
{
	public static class Config
	{
		public const string ServerUrl = "https://checkout-test.adyen.com/checkout/v49/";
		//Insert your test api key, public key and merchant account here
		public const string ApiKey = "";
		public const string MerchantAccount = "";
		public const string PublicKey = "";

		//These can be changed to your liking
		public const string CountryCode = "sv";
		public const string ShopperLocale = "en_GB";
		public const string Currency = "EUR";
	}

	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		RelativeLayout layout;
		TextView textViewLoading;
		FloatingActionButton fab;

		CardPaymentMethod paymentMethod;
		private decimal amount = 10;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Enabled = false;
			fab.Click += PayButton_Click;

			layout = FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
			textViewLoading = FindViewById<TextView>(Resource.Id.textViewLoading);

			InitPaymentForm();
		}

		private void PayButton_Click(object sender, EventArgs e)
		{
			View view = (View)sender;

			Task.Run(async () =>
			{
				var paymentResult = await DemoBackend.ExecutePayment(paymentMethod, "adyencheckout://com.companyname.demoappv3", amount);

				var message = paymentResult.ResultCode;
				if (!string.IsNullOrWhiteSpace(paymentResult.RefusalReason))
					message += $" - {paymentResult.RefusalReason} ({paymentResult.RefusalReasonCode})";
				RunOnUiThread(() => Snackbar.Make(view, message, Snackbar.LengthLong).Show());
			});
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

		private void InitPaymentForm()
		{
			Task.Run(async () =>
			{
				var paymentMethods = await DemoBackend.GetPaymentMethods(amount);
				var cardPaymentMethod = paymentMethods.FirstOrDefault(x => x.Type == "scheme");
				if (cardPaymentMethod == null)
				{
					RunOnUiThread(() => Snackbar.Make(layout, "Credit card payments not available!", Snackbar.LengthLong).Show());
					return;
				}

				var cardConfigurationBuilder = new CardConfiguration.Builder(this, Config.PublicKey);
				cardConfigurationBuilder.SetEnvironment(Com.Adyen.Checkout.Core.Api.Environment.Europe);
				var cardConfiguration = cardConfigurationBuilder.Build();

				RunOnUiThread(() =>
				{
					try
					{

						var cardComponent = CardComponent.Provider.Get(this, cardPaymentMethod, cardConfiguration) as CardComponent;

						var cardView = new CardView(this) { Id = View.GenerateViewId() };
						var cardLayout = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
						cardLayout.AddRule(LayoutRules.AlignParentTop);
						cardView.Attach(cardComponent, this);
						layout.AddView(cardView, cardLayout);
						textViewLoading.Visibility = ViewStates.Gone;

						cardComponent.Observe(this, new ObserverImpl
						{
							Changed = state =>
							{
								var paymentMethod = state.Data.PaymentMethod as CardPaymentMethod;

								Android.Util.Log.Debug("ObserverImpl", $"Onchanged -- Valid: {state.IsValid}, {paymentMethod.Type}");

								if (state.IsValid)
								{
									fab.Enabled = true;
									this.paymentMethod = paymentMethod;
								}
								else
									fab.Enabled = false;
							}
						});
					}
					catch (System.Exception ex)
					{
						Android.Util.Log.Error("MainActivity", Throwable.FromException(ex), ex.Message);
					}
				});


			});


		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		public class ObserverImpl : Java.Lang.Object, Android.Arch.Lifecycle.IObserver
		{
			public Action<Com.Adyen.Checkout.Base.PaymentComponentState> Changed;

			public void OnChanged(Java.Lang.Object p0)
			{
				if (p0 is Com.Adyen.Checkout.Base.PaymentComponentState state)
					Changed?.Invoke(state);
			}
		}
	}
}

