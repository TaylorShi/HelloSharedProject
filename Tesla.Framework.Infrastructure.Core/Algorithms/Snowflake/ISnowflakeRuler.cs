using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    public interface ISnowflakeRuler
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        SnowflakeOptions GetRulerInfo();
    }
}
