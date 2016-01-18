using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.ExceptionHandling
{
	public class ErrorMsgAttribute : Attribute
	{
		private string _message;
		public string Message { get { return _message; } }
		public ErrorMsgAttribute(string msg)
		{
			_message = msg;
		}
	}
}
