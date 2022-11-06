using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Core
{
    public class MessageException : Exception, IKnowException
    {
        public int ErrorCode { get; }

        public object[] ErrorData { get; }

        public MessageException(string message, int errorCode, object[] errorData = null) : base(message)
        {
            ErrorCode = errorCode;
            ErrorData = errorData;
        }
    }
}
