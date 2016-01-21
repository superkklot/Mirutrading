using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.ViewModel
{
	public class MessageBase
	{
		public MessageBase(int ecode, string emsg)
		{
			ErrorCode = ecode;
			ErrorMessage = emsg;
		}
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
