using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.IdBuilders
{
    /// <summary>
    /// 生成器接口
    /// </summary>
    public interface IGenerateProvider
    {
        /// <summary>
        /// 生成Id
        /// </summary>
        /// <returns></returns>
        long GenerateId();
    }
}
