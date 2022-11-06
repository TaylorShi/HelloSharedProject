using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.TenantModule
{
    /// <summary>
    /// 商户入参接口
    /// </summary>
    public interface ITenantVo
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string TenantId { get; set; }
    }
}
