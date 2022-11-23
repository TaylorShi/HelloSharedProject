using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.DataContract.Abstractions.QueryModule.VO
{
    /// <summary>
    /// 分页查询入参
    /// </summary>
    public class PageVo : IPageVo
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; } = 15;
    }
}
