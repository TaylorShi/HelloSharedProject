using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.DataContract.Abstractions.UserModule;

namespace Tesla.Framework.DataContract.Abstractions.TenantModule
{
    /// <summary>
    /// 商户查询基础入参
    /// </summary>
    public class PublicTenantVo : PublicUserVo, ITenantVo
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public string TenantId { get; set; }
    }
}
