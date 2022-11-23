using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.Domain.Abstractions.VersionBuilders;

namespace Tesla.Framework.Domain.Abstractions.BarcodeBuilders
{
    public class BarCodeBuilderPerSeqNo
    {
        /// <summary>
        /// 最大流水码
        /// </summary>
        protected const int MaxSequenceNo = 255;

        /// <summary>
        /// 开始时间
        /// </summary>
        protected static readonly DateTime Jan1st2019 = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 原子计数器
        /// </summary>
        protected readonly LoopAtomiCounter _loopAtomiCounter = null;

        /// <summary>
        /// 设备号
        /// </summary>
        public int CrarrierSeqNo { get; private set; }

        public BarCodeBuilderPerSeqNo(int crarrierSeqNo)
        {
            CrarrierSeqNo = crarrierSeqNo;
            _loopAtomiCounter = LoopAtomiCounter.GetInstance($"OrderBarCode_{crarrierSeqNo}", MaxSequenceNo);
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public string BuilderOrderBarCode(int instanceIndex)
        {
            return $"8{BarcodeBuilder(instanceIndex)}";
        }

        /// <summary>
        /// 生成退货单号
        /// </summary>
        /// <returns></returns>
        public string BuilderReturnOrderNo(int instanceIndex)
        {
            return $"6{BarcodeBuilder(instanceIndex)}";
        }

        /// <summary>
        /// 生成子订单号
        /// </summary>
        /// <returns></returns>
        public string BuilderSubOrderNo(int instanceIndex)
        {
            return $"S{BarcodeBuilder(instanceIndex)}";
        }

        /// <summary>
        /// 生成充值单号
        /// </summary>
        /// <returns></returns>
        public string BuilderRechargeOrderNo(int instanceIndex)
        {
            return $"C{BarcodeBuilder(instanceIndex)}";
        }

        /// <summary>
        /// 生成支付单号
        /// </summary>
        /// <returns></returns>
        public string BuilderPaymentOrderNo(int instanceIndex)
        {
            return $"P{BarcodeBuilder(instanceIndex)}";
        }

        /// <summary>
        /// (UTC时间2019-01-01 00:00到2036-01-05 14:48:31)+(0-1048575)+(0-255)
        /// 从UTC时间2019-9-27 11:31:04开始到18位
        /// 从UTC时间2026-5-18 19:10:44开始19位
        /// 从UTC时间2087-1-19 3:14:8开始日期溢出,单号会包含-号,位数从20为开始递减
        /// </summary>
        /// <returns></returns>
        protected virtual long BarcodeBuilder(int instanceIndex)
        {
            if (instanceIndex < 0 || instanceIndex > 15)
                throw new ArgumentException();
            var sequenceNo = _loopAtomiCounter.GetIncreaseNum();
            var secondTimeSpan = GetTimeSpanSecond();
            return (secondTimeSpan << 32) | ((long)instanceIndex) << 28 | ((long)CrarrierSeqNo) << 8 | (long)sequenceNo;
        }

        protected virtual long GetTimeSpanSecond()
        {
            return (long)(DateTime.UtcNow - Jan1st2019).TotalSeconds;
        }
    }
}
