using Android.Runtime;
using Android.Support.V7.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Adyen.Checkout.Base.UI.Adapter
{
    public partial class ClickableListRecyclerAdapter
    {
		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.base.ui.adapter']/class[@name='ClickableListRecyclerAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='ViewHolderT'] and parameter[2][@type='int']]"
		[Register("onBindViewHolder", "(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V", "GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler")]
		public override unsafe void OnBindViewHolder(RecyclerView.ViewHolder viewHolderT, int position)
		{
			const string __id = "onBindViewHolder.(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V";
			IntPtr native_viewHolderT = JNIEnv.ToLocalJniHandle(viewHolderT);
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[2];
				__args[0] = new JniArgumentValue(native_viewHolderT);
				__args[1] = new JniArgumentValue(position);
				_members.InstanceMethods.InvokeVirtualVoidMethod(__id, this, __args);
			}
			finally
			{
				JNIEnv.DeleteLocalRef(native_viewHolderT);
			}
		}
	}
}
