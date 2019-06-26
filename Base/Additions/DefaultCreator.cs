//using Android.Runtime;
//using Java.Interop;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Base.Additions
//{
//	// Metadata.xml XPath class reference: path="/api/package[@name='com.adyen.checkout.base.internal']/class[@name='JsonObject.DefaultCreator']"
//	[global::Android.Runtime.Register("com/adyen/checkout/base/internal/JsonObject$DefaultCreator", DoNotGenerateAcw = true)]
//	[global::Java.Interop.JavaTypeParameters(new string[] { "T extends com.adyen.checkout.base.internal.JsonObject" })]
//	protected internal sealed partial class DefaultCreator : global::Com.Adyen.Checkout.Base.Internal.JsonObject.Creator
//	{

//		internal new static readonly JniPeerMembers _members = new XAPeerMembers("com/adyen/checkout/base/internal/JsonObject$DefaultCreator", typeof(DefaultCreator));
//		internal static new IntPtr class_ref
//		{
//			get
//			{
//				return _members.JniPeerType.PeerReference.Handle;
//			}
//		}

//		public override global::Java.Interop.JniPeerMembers JniPeerMembers
//		{
//			get { return _members; }
//		}

//		protected override IntPtr ThresholdClass
//		{
//			get { return _members.JniPeerType.PeerReference.Handle; }
//		}

//		protected override global::System.Type ThresholdType
//		{
//			get { return _members.ManagedPeerType; }
//		}

//		internal DefaultCreator(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

//		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.base.internal']/class[@name='JsonObject.DefaultCreator']/method[@name='createFromJson' and count(parameter)=1 and parameter[1][@type='org.json.JSONObject']]"
//		[Register("createFromJson", "(Lorg/json/JSONObject;)Lcom/adyen/checkout/base/internal/JsonObject;", "")]
//		public override unsafe global::Java.Lang.Object CreateFromJson(global::Org.Json.JSONObject jsonObject)
//		{
//			const string __id = "createFromJson.(Lorg/json/JSONObject;)Lcom/adyen/checkout/base/internal/JsonObject;";
//			try
//			{
//				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
//				__args[0] = new JniArgumentValue((jsonObject == null) ? IntPtr.Zero : ((global::Java.Lang.Object)jsonObject).Handle);
//				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
//				return (Java.Lang.Object)global::Java.Lang.Object.GetObject<global::Java.Lang.Object>(__rm.Handle, JniHandleOwnership.TransferLocalRef);
//			}
//			finally
//			{
//			}
//		}

//		// Metadata.xml XPath method reference: path="/api/package[@name='com.adyen.checkout.base.internal']/class[@name='JsonObject.DefaultCreator']/method[@name='createFromJson' and count(parameter)=1 and parameter[1][@type='org.json.JSONObject']]"
//		[Register("newArray", "(I)[Lcom/adyen/checkout/base/internal/JsonObject;", "")]
//		public override unsafe global::Java.Lang.Object[] NewArray(int size)
//		{
//			const string __id = "newArray.(I)[Lcom/adyen/checkout/base/internal/JsonObject;";
//			try
//			{
//				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
//				__args[0] = new JniArgumentValue(size);
//				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod(__id, this, __args);
//				return (global::Java.Lang.Object[])JNIEnv.GetArray(__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof(global::Java.Lang.Object));
//			}
//			finally
//			{
//			}
//		}

//		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.adyen.checkout.base.internal']/class[@name='JsonObject.DefaultCreator']/constructor[@name='JsonObject.DefaultCreator' and count(parameter)=1 and parameter[1][@type='java.lang.Class&lt;T&gt;']]"
//		[Register(".ctor", "(Ljava/lang/Class;)V", "")]
//		public unsafe DefaultCreator(global::Java.Lang.Class clazz)
//			: base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
//		{
//			const string __id = "(Ljava/lang/Class;)V";

//			if (((global::Java.Lang.Object)this).Handle != IntPtr.Zero)
//				return;

//			try
//			{
//				JniArgumentValue* __args = stackalloc JniArgumentValue[1];
//				__args[0] = new JniArgumentValue((clazz == null) ? IntPtr.Zero : ((global::Java.Lang.Object)clazz).Handle);
//				var __r = _members.InstanceMethods.StartCreateInstance(__id, ((object)this).GetType(), __args);
//				SetHandle(__r.Handle, JniHandleOwnership.TransferLocalRef);
//				_members.InstanceMethods.FinishCreateInstance(__id, this, __args);
//			}
//			finally
//			{
//			}
//		}
//	}
//}
