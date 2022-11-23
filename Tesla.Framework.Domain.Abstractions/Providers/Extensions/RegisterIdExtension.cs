using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.IdBuilders.Extensions
{
    /// <summary>
    /// 注册ID扩展方法
    /// </summary>
    public static class RegisterIdExtension
    {
        /// <summary>
        /// 注册指定类型ID生成方法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="func"></param>
        public static void RegisterIdFunc<TKey>(Func<TKey> func)
        {
            IdGetterExtension.IdFuncsForType[typeof(TKey)] = () => func.Invoke();
        }

        /// <summary>
        /// 注册注定实体ID生成方法
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="func"></param>
        public static void RegisterIdFunc<TKey, TEntity>(Func<TKey> func)
            where TEntity : Entity<TKey>
        {
            IdGetterExtension.EntitiesIdFunc[typeof(TEntity)] = () => func.Invoke();
        }
    }
}
