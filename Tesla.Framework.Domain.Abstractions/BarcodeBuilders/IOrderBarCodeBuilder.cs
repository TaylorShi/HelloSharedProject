using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.BarcodeBuilders
{
    public interface IOrderBarCodeBuilder
    {
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <param name="crarrierSeqNo">载体编号</param>
        /// <returns></returns>
        string Build(int crarrierSeqNo);
    }
}
