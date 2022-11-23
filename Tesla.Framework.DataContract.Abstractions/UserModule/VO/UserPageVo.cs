using System;
using System.Collections.Generic;
using System.Text;
using Tesla.Framework.DataContract.Abstractions.QueryModule.VO;

namespace Tesla.Framework.DataContract.Abstractions.UserModule.VO
{
    /// <summary>
    /// 分页查询用户入参
    /// </summary>
    public class UserPageVo : PageVo, IUserVo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}
