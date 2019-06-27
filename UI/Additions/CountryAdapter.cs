using System;
using System.Linq;
using Android.Runtime;
using Java.Interop;

namespace Com.Adyen.Checkout.UI.Internal.Sepadirectdebit
{
	public partial class CountryAdapter
	{
		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.ui.internal.sepadirectdebit']/class[@name='CountryAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='com.adyen.checkout.ui.internal.sepadirectdebit.IbanSuggestionViewHolder'] and parameter[2][@type='int']]"
		[Register("onBindViewHolder", "(Lcom/adyen/checkout/ui/internal/sepadirectdebit/IbanSuggestionViewHolder;I)V", "GetOnBindViewHolder_Lcom_adyen_checkout_ui_internal_sepadirectdebit_IbanSuggestionViewHolder_IHandler")]
		public override unsafe void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
		{
			const string __id = "onBindViewHolder.(Lcom/adyen/checkout/ui/internal/sepadirectdebit/IbanSuggestionViewHolder;I)V";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[2];
				__args[0] = new JniArgumentValue((holder == null) ? IntPtr.Zero : ((global::Java.Lang.Object)holder).Handle);
				__args[1] = new JniArgumentValue(position);
				_members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
			}
			finally
			{
			}
		}
	}
}
