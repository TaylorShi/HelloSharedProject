using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.UserModule.VO
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserVo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        string UserId { get; set; }
    }
}
