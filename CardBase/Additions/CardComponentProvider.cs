using System;
using System.Linq;
using Android.Runtime;
using Java.Interop;

namespace Com.Adyen.Checkout.Card
{
	public partial class CardComponentProvider
	{
		//TODO: It might be a better idea to change the IPaymentComponentProvider interface in Base instead, so that it uses the correct classes instead of just Object, at least if these methods are called directly.

		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardComponentProvider']/method[@name='get' and count(parameter)=3 and parameter[1][@type='android.support.v4.app.Fragment'] and parameter[2][@type='com.adyen.checkout.base.model.paymentmethods.PaymentMethod'] and parameter[3][@type='com.adyen.checkout.card.CardConfiguration']]"
		[Register("get", "(Landroid/support/v4/app/Fragment;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;)Lcom/adyen/checkout/card/CardComponent;", "GetGet_Landroid_support_v4_app_Fragment_Lcom_adyen_checkout_base_model_paymentmethods_PaymentMethod_Lcom_adyen_checkout_card_CardConfiguration_Handler")]
		public virtual unsafe global::Java.Lang.Object Get(global::Android.Support.V4.App.Fragment fragment, global::Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod paymentMethod, global::Java.Lang.Object configuration)
		{
			const string __id = "get.(Landroid/support/v4/app/Fragment;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;)Lcom/adyen/checkout/card/CardComponent;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[3];
				__args[0] = new JniArgumentValue((fragment == null) ? IntPtr.Zero : ((global::Java.Lang.Object)fragment).Handle);
				__args[1] = new JniArgumentValue((paymentMethod == null) ? IntPtr.Zero : ((global::Java.Lang.Object)paymentMethod).Handle);
				__args[2] = new JniArgumentValue((configuration == null) ? IntPtr.Zero : ((global::Java.Lang.Object)configuration).Handle);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Adyen.Checkout.Card.CardComponent>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardComponentProvider']/method[@name='get' and count(parameter)=3 and parameter[1][@type='android.support.v4.app.FragmentActivity'] and parameter[2][@type='com.adyen.checkout.base.model.paymentmethods.PaymentMethod'] and parameter[3][@type='com.adyen.checkout.card.CardConfiguration']]"
		[Register("get", "(Landroid/support/v4/app/FragmentActivity;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;)Lcom/adyen/checkout/card/CardComponent;", "GetGet_Landroid_support_v4_app_FragmentActivity_Lcom_adyen_checkout_base_model_paymentmethods_PaymentMethod_Lcom_adyen_checkout_card_CardConfiguration_Handler")]
		public virtual unsafe global::Java.Lang.Object Get(global::Android.Support.V4.App.FragmentActivity activity, global::Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod paymentMethod, global::Java.Lang.Object configuration)
		{
			const string __id = "get.(Landroid/support/v4/app/FragmentActivity;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;)Lcom/adyen/checkout/card/CardComponent;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[3];
				__args[0] = new JniArgumentValue((activity == null) ? IntPtr.Zero : ((global::Java.Lang.Object)activity).Handle);
				__args[1] = new JniArgumentValue((paymentMethod == null) ? IntPtr.Zero : ((global::Java.Lang.Object)paymentMethod).Handle);
				__args[2] = new JniArgumentValue((configuration == null) ? IntPtr.Zero : ((global::Java.Lang.Object)configuration).Handle);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Adyen.Checkout.Card.CardComponent>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardComponentProvider']/method[@name='isAvailable' and count(parameter)=4 and parameter[1][@type='android.app.Application'] and parameter[2][@type='com.adyen.checkout.base.model.paymentmethods.PaymentMethod'] and parameter[3][@type='com.adyen.checkout.card.CardConfiguration'] and parameter[4][@type='com.adyen.checkout.base.ComponentAvailableCallback&lt;com.adyen.checkout.card.CardConfiguration&gt;']]"
		[Register("isAvailable", "(Landroid/app/Application;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;Lcom/adyen/checkout/base/ComponentAvailableCallback;)V", "GetIsAvailable_Landroid_app_Application_Lcom_adyen_checkout_base_model_paymentmethods_PaymentMethod_Lcom_adyen_checkout_card_CardConfiguration_Lcom_adyen_checkout_base_ComponentAvailableCallback_Handler")]
		public virtual unsafe void IsAvailable(global::Android.App.Application applicationContext, global::Com.Adyen.Checkout.Base.Model.Paymentmethods.PaymentMethod paymentMethod, global::Java.Lang.Object configuration, global::Com.Adyen.Checkout.Base.IComponentAvailableCallback @callback)
		{
			const string __id = "isAvailable.(Landroid/app/Application;Lcom/adyen/checkout/base/model/paymentmethods/PaymentMethod;Lcom/adyen/checkout/card/CardConfiguration;Lcom/adyen/checkout/base/ComponentAvailableCallback;)V";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[4];
				__args[0] = new JniArgumentValue((applicationContext == null) ? IntPtr.Zero : ((global::Java.Lang.Object)applicationContext).Handle);
				__args[1] = new JniArgumentValue((paymentMethod == null) ? IntPtr.Zero : ((global::Java.Lang.Object)paymentMethod).Handle);
				__args[2] = new JniArgumentValue((configuration == null) ? IntPtr.Zero : ((global::Java.Lang.Object)configuration).Handle);
				__args[3] = new JniArgumentValue((@callback == null) ? IntPtr.Zero : ((global::Java.Lang.Object)@callback).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
			}
			finally
			{
			}
		}
	}
}
