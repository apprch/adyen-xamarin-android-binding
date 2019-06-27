using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Adyen.Checkout.UI.Internal.Common.Util
{
    public partial class PayButtonUtil : global::Java.Lang.Object
	{
		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.ui.internal.common.util']/class[@name='PayButtonUtil']/method[@name='setPayButtonText' and count(parameter)=4 and parameter[1][@type='T'] and parameter[2][@type='com.adyen.checkout.core.model.PaymentMethod'] and parameter[3][@type='android.widget.TextView'] and parameter[4][@type='android.widget.TextView']]"
		[Register("setPayButtonText", "(Landroid/support/v7/app/AppCompatActivity;Lcom/adyen/checkout/core/model/PaymentMethod;Landroid/widget/TextView;Landroid/widget/TextView;)V", "")]
		[global::Java.Interop.JavaTypeParameters(new string[] { "T extends android.support.v7.app.AppCompatActivity & com.adyen.checkout.ui.internal.common.model.CheckoutSessionProvider" })]
		public static unsafe void SetPayButtonText(global::Java.Lang.Object checkoutSessionActivity, global::Com.Adyen.Checkout.Core.Model.IPaymentMethod paymentMethod, global::Android.Widget.TextView payButton, global::Android.Widget.TextView surchargeTextView)
		{
			const string __id = "setPayButtonText.(Landroid/support/v7/app/AppCompatActivity;Lcom/adyen/checkout/core/model/PaymentMethod;Landroid/widget/TextView;Landroid/widget/TextView;)V";
			IntPtr native_checkoutSessionActivity = JNIEnv.ToLocalJniHandle(checkoutSessionActivity);
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[4];
				__args[0] = new JniArgumentValue(native_checkoutSessionActivity);
				__args[1] = new JniArgumentValue((paymentMethod == null) ? IntPtr.Zero : ((global::Java.Lang.Object)paymentMethod).Handle);
				__args[2] = new JniArgumentValue((payButton == null) ? IntPtr.Zero : ((global::Java.Lang.Object)payButton).Handle);
				__args[3] = new JniArgumentValue((surchargeTextView == null) ? IntPtr.Zero : ((global::Java.Lang.Object)surchargeTextView).Handle);
				_members.StaticMethods.InvokeVoidMethod(__id, __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef(native_checkoutSessionActivity);
			}
		}
	}
}
