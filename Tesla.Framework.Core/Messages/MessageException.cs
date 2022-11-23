using System;

namespace Tesla.Framework.Core.Messages
{
    /// <summary>
    /// 消息异常
    /// </summary>
    public class MessageException : Exception, IKnowException
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// 错误数据
        /// </summary>
        public object[] ErrorData { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorData"></param>
        public MessageException(string message, int errorCode, object[] errorData = null) : base(message)
        {
            ErrorCode = errorCode;
            ErrorData = errorData;
        }
    }
}
