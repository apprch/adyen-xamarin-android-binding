﻿using System;
using System.Linq;

namespace DemoAppV3.Model
{
	public class PaymentsResponse
	{
		public string ResultCode { get; set; }
		public ActionData Action { get; set; }
		public AuthenticationData Authentication { get; set; }
		public Detail[] Details { get; set; }
		public string PaymentData { get; set; }
		public RedirectData Redirect { get; set; }
		public string RefusalReason { get; set; }
		public string RefusalReasonCode { get; set; }

		public class ActionData
		{
			public string PaymentData { get; set; }
			public string PaymentMethodType { get; set; }
			public string Token { get; set; }
			public string Type { get; set; }
		}

		public class AuthenticationData
		{
			public string ThreeDS2FingerprintToken { get; set; }
		}

		public class Detail
		{
			public string Key { get; set; }
			public string Type { get; set; }
		}

		public class RedirectData
		{
			public object Data { get; set; }
			public string Method { get; set; }
			public string Url { get; set; }
		}
	}
}

