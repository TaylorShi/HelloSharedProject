using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.DataContract.Abstractions.UserModule.VO;

namespace Tesla.Framework.DataContract.Abstractions.TenantModule.VO
{
    /// <summary>
    /// 商户查询基础入参
    /// </summary>
    public class TenantVo : UserVo, ITenantVo
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public string TenantId { get; set; }
    }
}
