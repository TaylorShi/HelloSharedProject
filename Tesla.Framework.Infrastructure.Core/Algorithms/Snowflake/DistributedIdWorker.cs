using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.Core.Messages;
using Tesla.Framework.Domain.Abstractions.IdBuilders;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    /// <summary>
    /// 分布式Id生成器
    /// </summary>
    public class DistributedIdWorker : IIdWorker
    {
        /// <summary>
        /// 时间随机量，不能大于当前时间戳
        /// </summary>
        private const long Twepoch = 815818088000L;

        //互斥体
        private readonly object _mutex = new object();

        private SnowflakeOptions _rulerInfo;

        /// <summary>
        /// 上次的时间戳
        /// </summary>
        private long _lastTimestamp;
        /// <summary>
        /// 流水号
        /// </summary>
        private int _sequence;

        private readonly IOptions<SnowflakeOptions> _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snowflakeRuler"></param>
        public DistributedIdWorker(IOptions<SnowflakeOptions> options)
        {
            _options = options;
        }

        /// <summary>
        /// 获取下一个Id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            if (_rulerInfo == null)
            {
                lock (_mutex)
                {
                    if (_rulerInfo == null)
                    {
                        var rulerInfo = _options.Value;

                        if (rulerInfo.SequenceBits < 6)
                            throw new ArgumentException($"流水号位数不能小于6");

                        if ((rulerInfo.WorkerIdBits + rulerInfo.SequenceBits) > 22)
                            throw new ArgumentException($"workIdbits+sequenceBits={rulerInfo.WorkerIdBits + rulerInfo.SequenceBits}大于22，越界!");

                        if (rulerInfo.WorkerId >= Math.Pow(2, rulerInfo.WorkerIdBits))
                            throw new ArgumentException($"WorkerId:{rulerInfo.WorkerIdBits}越界,Max WorkerId:{Math.Pow(2, rulerInfo.WorkerIdBits)}");

                        _rulerInfo = rulerInfo;
                    }
                }
            }

            lock (_mutex)
            {
                var timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                // 时间戳不合法
                if (timestamp < _lastTimestamp)
                    throw new MessageException($"时钟回拨.  Refusing to generate id for {_lastTimestamp - timestamp} milliseconds", 9999);

                if (timestamp == _lastTimestamp)
                    _sequence++;
                else
                {
                    _sequence = 0;
                    _lastTimestamp = timestamp;
                }

#pragma warning disable CS0675 // 对进行了带符号扩展的操作数使用了按位或运算符
                var id = ((_lastTimestamp - Twepoch) << _rulerInfo.TimeStampOffsets) |
                    (_rulerInfo.WorkerId << _rulerInfo.SequenceBits) | _sequence;
#pragma warning restore CS0675 // 对进行了带符号扩展的操作数使用了按位或运算符

                return id;
            }
        }
    }
}
