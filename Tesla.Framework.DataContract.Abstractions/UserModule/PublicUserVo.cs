using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.UserModule
{
    /// <summary>
    /// 用户查询基础入参
    /// </summary>
    public class PublicUserVo : IUserIdVo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
