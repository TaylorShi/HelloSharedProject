using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.DataContract.Abstractions.UserModule.VO;

namespace Tesla.Framework.DataContract.Abstractions.TenantModule.VO
{
    /// <summary>
    /// 分页查询商户入参
    /// </summary>
    public class TenantPageVo : UserPageVo, ITenantVo
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string TenantId { get; set; }
    }
}
