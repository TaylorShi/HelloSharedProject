using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.IdBuilders
{
    /// <summary>
    /// ID生成器接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IIdBuilder<TKey, TAggregateRoot>
            where TAggregateRoot : Entity<TKey>
    {
        /// <summary>
        /// 创建Id
        /// </summary>
        /// <returns></returns>
        TKey BuildId();
    }
}
