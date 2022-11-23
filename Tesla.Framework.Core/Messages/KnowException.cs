
namespace Tesla.Framework.Core.Messages
{
    /// <summary>
    /// 已知异常
    /// </summary>
    public class KnowException : IKnowException
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// 错误数据
        /// </summary>
        public object[] ErrorData { get; private set; }

        /// <summary>
        /// 未知异常
        /// </summary>
        public readonly static IKnowException Unknown = new KnowException { Message = "未知错误", ErrorCode = 9999 };

        /// <summary>
        /// 从接口转换
        /// </summary>
        /// <param name="knowException"></param>
        /// <returns></returns>
        public static IKnowException FromKnowException(IKnowException knowException)
        {
            return new KnowException { Message = knowException.Message, ErrorCode = knowException.ErrorCode, ErrorData = knowException.ErrorData };
        }
    }
}
