﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.ExceptionHandling
{
	public enum ErrorCode
	{
		[ErrorMsg("系统错误")]
		SystemError = 1,
	}
}
