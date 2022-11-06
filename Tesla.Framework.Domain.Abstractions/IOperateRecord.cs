using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions
{
    /// <summary>
    /// 操作记录
    /// </summary>
    public interface IOperateRecord
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreateOn { get; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        string CreateBy { get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdateOn { get; }

        /// <summary>
        /// 更新者Id
        /// </summary>
        string UpdateBy { get; }
    }
}
