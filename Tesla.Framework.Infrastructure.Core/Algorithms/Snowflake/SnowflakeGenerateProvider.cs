using Microsoft.Extensions.Options;
using System;
using Tesla.Framework.Domain.Abstractions.IdBuilders;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    /// <summary>
    /// 雪花算法生成器
    /// </summary>
    public class SnowflakeGenerateProvider : IGenerateProvider
    {
        // 基准时间
        const long Twepoch = 943891200000L;

        // 机器ID位数
        const int WorkerIdBits = 5;

        // 机房ID位数
        const int DataCenterIdBits = 5;

        // 序列号位数
        const int SequenceBits = 12;

        // 机器ID单机房最小值
        const int MinWorkerId = 0;

        // 机器ID单机房最大值
        const long MaxWorkerId = -1L ^ -1L << WorkerIdBits;

        // 机房ID最小值
        const int MinDataCenterId = 0;

        // 机房ID最大值
        const long MaxDataCenterId = -1L ^ -1L << DataCenterIdBits;

        // 序列号ID最大值
        const long SequenceMask = -1L ^ -1L << SequenceBits;

        // 机器ID偏左移12位
        private const int WorkerIdLeftShift = SequenceBits;

        // 机房ID偏左移17位
        private const int DataCenterIdLeftShift = SequenceBits + WorkerIdBits;

        // 时间戳左移22位
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DataCenterIdBits;

        /// <summary>
        /// 当前序列号
        /// </summary>
        private long Sequence { get; set; } = 0L;

        /// <summary>
        /// 上一次时间戳
        /// </summary>
        private long LastTimestamp = -1L;

        /// <summary>
        /// 当前机器ID
        /// </summary>
        private readonly long WorkerId = 1L;

        /// <summary>
        /// 当前机房ID
        /// </summary>
        private readonly long DataCenterId = 1L;

        /// <summary>
        /// 生成锁
        /// </summary>
        private readonly object _generateLock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="ArgumentException"></exception>
        public SnowflakeGenerateProvider(IOptions<SnowflakeOptions> options)
        {
            WorkerId = options.Value.WorkerId;
            if (WorkerId < MinWorkerId || WorkerId > MaxWorkerId)
            {
                throw new ArgumentException(string.Format("机器ID不得小于{0}且不得大于{1}", MinWorkerId, MaxWorkerId));
            }

            DataCenterId = options.Value.DataCenterId;
            if (DataCenterId < MinDataCenterId || DataCenterId > MaxDataCenterId)
            {
                throw new ArgumentException(string.Format("机房ID不得小于{0}且不得大于{1}", MinDataCenterId, MaxDataCenterId));
            }
        }

        /// <summary>
        /// 生成Id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long GenerateId()
        {
            lock (_generateLock)
            {
                // 获取当前时间戳
                var timestamp = GetCurrentTimestamp();
                if (timestamp < LastTimestamp)
                {
                    throw new ArgumentException(string.Format("当前时间戳必须大于上一次时间戳，已拒绝为{0}毫秒生成雪花ID", LastTimestamp - timestamp));
                }

                // 如果上一次时间戳和当前时间戳相等(同一个毫秒内)
                if (LastTimestamp == timestamp)
                {
                    // 启用序列号自增机制，并且和序列号最大值相与，去掉高位
                    Sequence = Sequence + 1 & SequenceMask;
                    
                    // 如果自增已经超出了序列号最大值，就进入下一个毫秒循环
                    if (Sequence == 0)
                    {
                        // 等待下一个毫秒
                        timestamp = UntilNextTimestamp(LastTimestamp);
                    }
                }
                else
                {
                    // 获取起始序列号
                    Sequence = GetDefaultSequence();
                }

                LastTimestamp = timestamp;
                return timestamp - Twepoch << TimestampLeftShift | DataCenterId << DataCenterIdLeftShift | WorkerId << WorkerIdLeftShift | Sequence;
            }
        }

        /// <summary>
        /// 获取起始序列号
        /// </summary>
        /// <returns></returns>
        private long GetDefaultSequence()
        {
            // 正常应该从0L开始，但是这里做个随机数，增加随机性
            return new Random().Next(10);
        }

        /// <summary>
        /// 等待下一个毫秒
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long UntilNextTimestamp(long lastTimestamp)
        {
            var timestamp = GetCurrentTimestamp();
            // 防止之前时间比当前时间更小
            while (timestamp <= lastTimestamp)
            {
                timestamp = GetCurrentTimestamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        private long GetCurrentTimestamp()
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
