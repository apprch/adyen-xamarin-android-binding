using Android.Runtime;
using Android.Widget;
using Com.Adyen.Checkout.Base;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Adyen.Checkout.Card
{
    public partial class CardView
	{
		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardView']/method[@name='onChanged' and count(parameter)=1 and parameter[1][@type='com.adyen.checkout.card.data.output.CardOutputData']]"
		[Register("onChanged", "(Lcom/adyen/checkout/card/data/output/CardOutputData;)V", "")]
		public unsafe void OnChanged(global::Java.Lang.Object cardOutputData)
		{
			const string __id = "onChanged.(Lcom/adyen/checkout/card/data/output/CardOutputData;)V";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
				__args[0] = new JniArgumentValue((cardOutputData == null) ? IntPtr.Zero : ((global::Java.Lang.Object)cardOutputData).Handle);
				_members.InstanceMethods.InvokeAbstractVoidMethod(__id, this, __args);
			}
			finally
			{
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardView']/method[@name='attach' and count(parameter)=2 and parameter[1][@type='com.adyen.checkout.card.CardComponent'] and parameter[2][@type='android.arch.lifecycle.LifecycleOwner']]"
		[Register("attach", "(Lcom/adyen/checkout/card/CardComponent;Landroid/arch/lifecycle/LifecycleOwner;)V", "")]
		public unsafe void Attach(global::Java.Lang.Object component, global::Android.Arch.Lifecycle.ILifecycleOwner lifecycleOwner)
		{
			const string __id = "attach.(Lcom/adyen/checkout/card/CardComponent;Landroid/arch/lifecycle/LifecycleOwner;)V";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[2];
				__args[0] = new JniArgumentValue((component == null) ? IntPtr.Zero : ((global::Java.Lang.Object)component).Handle);
				__args[1] = new JniArgumentValue((lifecycleOwner == null) ? IntPtr.Zero : ((global::Java.Lang.Object)lifecycleOwner).Handle);
				_members.InstanceMethods.InvokeAbstractVoidMethod(__id, this, __args);
			}
			finally
			{
			}
		}
	}
}
