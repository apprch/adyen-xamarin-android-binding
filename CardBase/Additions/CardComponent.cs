using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Adyen.Checkout.Card
{
    public partial class CardComponent
    {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.card']/class[@name='CardComponent']/method[@name='onInputDataChanged' and count(parameter)=1 and parameter[1][@type='com.adyen.checkout.card.data.input.CardInputData']]"
		[Register("onInputDataChanged", "(Lcom/adyen/checkout/card/data/input/CardInputData;)Lcom/adyen/checkout/card/data/output/CardOutputData;", "")]
		protected override unsafe global::Java.Lang.Object OnInputDataChanged(global::Java.Lang.Object inputData)
		{
			const string __id = "onInputDataChanged.(Lcom/adyen/checkout/card/data/input/CardInputData;)Lcom/adyen/checkout/card/data/output/CardOutputData;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
				__args[0] = new JniArgumentValue((inputData == null) ? IntPtr.Zero : ((global::Java.Lang.Object)inputData).Handle);
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Adyen.Checkout.Card.Data.Output.CardOutputData>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
			}
		}
	}
}
