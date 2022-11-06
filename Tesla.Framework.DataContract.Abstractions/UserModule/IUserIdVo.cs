using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.UserModule
{
    /// <summary>
    /// 用户入参接口
    /// </summary>
    public interface IUserIdVo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        string UserId { get; set; }
    }
}
