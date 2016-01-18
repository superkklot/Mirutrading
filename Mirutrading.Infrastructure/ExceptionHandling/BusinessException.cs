using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.ExceptionHandling
{
	public class BusinessException : Exception
	{
		private int _errorCode;

		public int ErrorCode { get { return _errorCode; } }

		public BusinessException(ErrorCode errorCode)
			: this(errorCode, errorCode.GetMessage())
		{
		}

		public BusinessException(ErrorCode errorCode, string msg)
			: base(msg)
		{
			_errorCode = (int)errorCode;
		}
	}
}
