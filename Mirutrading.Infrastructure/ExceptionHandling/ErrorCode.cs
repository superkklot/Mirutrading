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

		[ErrorMsg("请求的参数错误")]
		RequestInvalid = 2,
	}

    public enum OneErrorCode
    {
        [ErrorMsg("100")]
        OneError = 100,

        [ErrorMsg("100")]
        OneError2 = 101,
    }
}
