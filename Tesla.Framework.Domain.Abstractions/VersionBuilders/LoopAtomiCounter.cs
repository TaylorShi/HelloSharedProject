using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tesla.Framework.Domain.Abstractions.VersionBuilders
{
    /// <summary>
    /// 循环计数器
    /// </summary>
    public class LoopAtomiCounter
    {
        private static readonly ConcurrentDictionary<string, LoopAtomiCounter>
            AtomiCounters = new ConcurrentDictionary<string, LoopAtomiCounter>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instKey">实例key</param>
        /// <param name="maxCount">最大数量</param>
        /// <returns></returns>
        public static LoopAtomiCounter GetInstance(string instKey, int maxCount = int.MaxValue)
        {
            return AtomiCounters.GetOrAdd(instKey, new LoopAtomiCounter(maxCount));
        }

        private static readonly object _mutex = new object();
        private int _count = -1;
        private readonly int _maxCount;

        private LoopAtomiCounter(int maxCount)
        {
            _maxCount = maxCount;
        }

        /// <summary>
        /// 获取递增数字
        /// </summary>
        /// <returns></returns>
        public int GetIncreaseNum()
        {
            if (_count + 1 >= _maxCount)
            {
                lock (_mutex)
                {
                    if (_count + 1 > _maxCount)
                        _count = -1;
                }
            }

            return Interlocked.Increment(ref _count);
        }
    }
}
