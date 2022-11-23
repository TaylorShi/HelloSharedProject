using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    public static class DistributedIdExtension
    {
        /// <summary>
        /// 获取分布式Id元信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DistributedIdInfo GetIdInformation(this long id)
        {
            var bitsOfId = Convert.ToString(id, 2);
            //流水号
            var bitOfSeq = bitsOfId.Substring(bitsOfId.Length - 7, 7);
            var seqNo = Convert.ToInt32(bitOfSeq, 2);
            //实例编号
            var bitOfInstId = bitsOfId.Substring(bitsOfId.Length - 11, 4);
            var instId = Convert.ToInt32(bitOfInstId, 2);
            //服务编号
            var bitOfServiceNo = bitsOfId.Substring(bitsOfId.Length - 19, 8);
            var serviceNo = Convert.ToInt32(bitOfServiceNo, 2);
            //数据中心Id
            var bitOfCenterId = bitsOfId.Substring(bitsOfId.Length - 22, 3);
            var centerId = Convert.ToInt32(bitOfCenterId, 2);
            //时间戳
            var timeStamp = (id >> 22) + 815818088000L;

            return new DistributedIdInfo
            {
                SequenceNo = seqNo,
                InstanceId = instId,
                ServiceNo = serviceNo,
                DataCenterId = centerId,
                Timestamp = timeStamp
            };
        }
    }

    /// <summary>
    /// 分布式信息
    /// </summary>
    public struct DistributedIdInfo
    {
        /// <summary>
        /// 数据中心Id
        /// </summary>
        public int DataCenterId { get; set; }

        /// <summary>
        /// 服务Id
        /// </summary>
        public int ServiceNo { get; set; }

        /// <summary>
        /// 实例Id
        /// </summary>
        public int InstanceId { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public int SequenceNo { get; set; }

        /// <summary>
        /// 时间戳.毫秒级
        /// </summary>
        public long Timestamp { get; set; }
    }
}
