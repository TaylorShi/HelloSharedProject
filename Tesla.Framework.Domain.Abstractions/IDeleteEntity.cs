using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface IDeleteEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; }
    }
}
