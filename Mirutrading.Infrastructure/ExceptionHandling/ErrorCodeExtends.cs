using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.ExceptionHandling
{
	public static class ErrorCodeExtends
	{
		public static string GetMessage(this ErrorCode value)
		{
			string msg = "";
			Type type = value.GetType();
			FieldInfo fieldInfo = type.GetField(value.ToString());
			if (fieldInfo == null) return msg;

			var attrs = fieldInfo.GetCustomAttributes(typeof(ErrorMsgAttribute), false);
			if (attrs == null) return msg;
			ErrorMsgAttribute[] emAttr = attrs as ErrorMsgAttribute[];
			if (emAttr == null || emAttr.Length == 0) return msg;

			return emAttr[0].Message;
		}

        public static string GetMessage<T>(this T value)
            where T : struct
        {
            string msg = "";
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            if (fieldInfo == null) return msg;

            var attrs = fieldInfo.GetCustomAttributes(typeof(ErrorMsgAttribute), false);
            if (attrs == null) return msg;
            ErrorMsgAttribute[] emAttr = attrs as ErrorMsgAttribute[];
            if (emAttr == null || emAttr.Length == 0) return msg;

            return emAttr[0].Message;
        }

        public static int ToInt<T>(this T value)
            where T : struct
        {
            int retValue = Convert.ToInt32(value);
            return retValue;
        }

	}
}
