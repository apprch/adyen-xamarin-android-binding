﻿using System;
using System.Linq;

namespace DemoAppV3.Model
{
	public class ErrorResponse
	{
		public string Type { get; set; }
		public string ErrorCode { get; set; }
		public string Message { get; set; }
		public string ErrorType { get; set; }
	}
}

