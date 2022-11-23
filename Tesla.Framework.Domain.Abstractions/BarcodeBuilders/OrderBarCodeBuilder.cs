using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.BarcodeBuilders
{
    public class OrderBarCodeBuilder : AbstractBuilder, IOrderBarCodeBuilder
    {
        int instanceIndex = 0;

        public OrderBarCodeBuilder()
        {
        }

        protected override string Invoke(BarCodeBuilderPerSeqNo obj)
        {
            return obj.BuilderOrderBarCode(instanceIndex);
        }
    }
}
