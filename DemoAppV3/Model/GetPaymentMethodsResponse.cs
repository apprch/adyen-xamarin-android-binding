using System;
using System.Collections.Generic;
using System.Linq;
namespace DemoAppV3.Model
{
	public class GetPaymentMethodsResponse
	{
		public List<PaymentMethod> PaymentMethods { get; set; }

		public class PaymentMethod
		{
			public string Name { get; set; }
			public string Type { get; set; }
			public bool SupportsRecurring { get; set; }
		}
	}
}

