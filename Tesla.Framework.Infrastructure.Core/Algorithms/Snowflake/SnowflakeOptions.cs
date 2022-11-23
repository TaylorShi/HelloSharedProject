
namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    /// <summary>
    /// 雪花算法配置选项
    /// </summary>
    public class SnowflakeOptions
    {
        /// <summary>
        /// 机器ID
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// 机房ID
        /// </summary>
        public int DataCenterId { get; set; }
    }
}
