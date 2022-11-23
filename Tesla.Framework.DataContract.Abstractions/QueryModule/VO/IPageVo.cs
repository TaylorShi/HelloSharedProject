using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.QueryModule.VO
{
    /// <summary>
    /// 分页查询接口
    /// </summary>
    public interface IPageVo
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        int PageSize { get; set; }
    }
}
