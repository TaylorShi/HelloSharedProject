namespace Tesla.Framework.Core.Messages
{
    /// <summary>
    /// 已知异常接口
    /// </summary>
    public interface IKnowException
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// 错误数据
        /// </summary>
        public object[] ErrorData { get; }
    }
}
