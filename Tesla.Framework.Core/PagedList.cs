using System;
using System.Collections.Generic;

namespace Tesla.Framework.Core
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class PagedList<TData>
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public IEnumerable<TData> Data { get; set; }

        /// <summary>
        /// 分页序号
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }

        public PagedList(IEnumerable<TData> dataSource, int recordCount, int pageIndex, int pageSize)
        {
            Data = dataSource;
            RecordCount = recordCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            if (pageSize == 0)
            {
                PageCount = 0;
            }
            else
            {
                PageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            }
        }
    }
}
