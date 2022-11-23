using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.VersionBuilders
{
    public class VersionNoBuilder<TEntity> : IVersionNoBuilder<TEntity>
            where TEntity : IEntity
    {
        private const int SequenceBits = 10;

        private readonly LoopAtomiCounter loopAtomiCounter =
            LoopAtomiCounter.GetInstance(typeof(TEntity).FullName, (int)Math.Pow(2, SequenceBits) - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long CreateVersionNo()
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;

            int getCount = loopAtomiCounter.GetIncreaseNum();
            long versionNo = (timeStamp << SequenceBits) | getCount;
            return versionNo;
        }
    }
}
