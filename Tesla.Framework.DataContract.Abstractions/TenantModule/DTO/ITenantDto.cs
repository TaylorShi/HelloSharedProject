using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.TenantModule.DTO
{
    /// <summary>
    /// 商户信息接口
    /// </summary>
    public interface ITenantDto
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        string TenantId { get; set; }
    }
}
