using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tesla.Framework.Core;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tesla.Framework.Infrastructure.Core.Extensions
{
    /// <summary>
    /// LINQ查询扩展
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TDomainModel"></typeparam>
        /// <typeparam name="TDataModel"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="configuration"></param>
        /// <param name="maxPageSize"></param>
        /// <param name="defaultPageSize"></param>
        /// <param name="defaultPageIndex"></param>
        /// <returns></returns>
        public static async Task<PagedList<TDataModel>> Paged<TDomainModel,TDataModel>(this IQueryable<TDomainModel> query, int pageIndex, int pageSize, AutoMapper.IConfigurationProvider configuration,int maxPageSize=200,int defaultPageSize=15,int defaultPageIndex =1)
        {
            if (pageIndex <= 0)
            {
                pageIndex = defaultPageIndex;
            }
            if (pageSize <= 0 || pageSize > maxPageSize)
            {
                pageSize = defaultPageSize;
            }
            var resultCount = await query.CountAsync();
            if (pageSize * (pageIndex - 1) >= resultCount)
            {
                return new PagedList<TDataModel>(new List<TDataModel>(), resultCount, pageIndex, pageSize);
            }
            var items = await query.PageBy(pageIndex - 1, pageSize).ProjectTo<TDataModel>(configuration).ToListAsync();
            return new PagedList<TDataModel>(items, resultCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }

        /// <summary>
        /// 根据If条件筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        /// <summary>
        /// 根据If条件筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
