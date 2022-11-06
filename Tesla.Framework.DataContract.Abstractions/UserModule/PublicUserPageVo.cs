using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.DataContract.Abstractions.QueryModule;

namespace Tesla.Framework.DataContract.Abstractions.UserModule
{
    /// <summary>
    /// 分页查询用户入参
    /// </summary>
    public class PublicUserPageVo : PublicPageVo, IUserIdVo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}
