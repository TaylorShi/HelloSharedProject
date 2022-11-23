using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.VersionBuilders
{
    /// <summary>
    /// 版本获取器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IVersionNoBuilder<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        long CreateVersionNo();
    }
}
