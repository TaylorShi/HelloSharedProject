using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions
{
    /// <summary>
    /// 租户身份
    /// </summary>
    public interface ITenantIdentity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        long TenantId { get; }
    }
}
