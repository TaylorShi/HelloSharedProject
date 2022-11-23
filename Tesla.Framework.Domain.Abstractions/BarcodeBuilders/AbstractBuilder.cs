using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.BarcodeBuilders
{
    public abstract class AbstractBuilder
    {
        protected static readonly ConcurrentDictionary<int, BarCodeBuilderPerSeqNo> _orderBarCodeBuilder = new ConcurrentDictionary<int, BarCodeBuilderPerSeqNo>();

        protected const int MaxCrarrierSeqNo = 1048576;
        protected readonly object _mutex = new object();

        public AbstractBuilder()
        {

        }

        public string Build(int crarrierSeqNo)
        {
            if (crarrierSeqNo >= MaxCrarrierSeqNo)
                throw new ArgumentException();
            if (!_orderBarCodeBuilder.ContainsKey(crarrierSeqNo))
            {
                _orderBarCodeBuilder[crarrierSeqNo] = new BarCodeBuilderPerSeqNo(crarrierSeqNo);
            }
            return Invoke(_orderBarCodeBuilder[crarrierSeqNo]);
        }

        protected abstract string Invoke(BarCodeBuilderPerSeqNo obj);
    }
}
