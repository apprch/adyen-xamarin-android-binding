using Android.Runtime;
using Android.Support.V7.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Java.Interop;

namespace Com.Adyen.Checkout.Card
{
    public partial class CardListAdapter
    {
		[Register("onBindViewHolder", "(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;", "GetOnBindViewHolder_Landroid_view_ViewGroup_IHandler")]
		public override unsafe void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			const string __id = "onBindViewHolder.(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;";
			try
			{
				JniArgumentValue* __args = stackalloc JniArgumentValue[2];
				__args[0] = new JniArgumentValue((holder == null) ? IntPtr.Zero : ((global::Java.Lang.Object)holder).Handle);
				__args[1] = new JniArgumentValue(position);
				_members.InstanceMethods.InvokeVirtualObjectMethod(__id, this, __args);
			}
			finally
			{
			}
		}
	}
}
