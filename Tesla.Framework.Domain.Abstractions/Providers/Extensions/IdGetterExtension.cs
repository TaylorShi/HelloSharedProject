using System;
using System.Collections.Generic;

namespace Tesla.Framework.Domain.Abstractions.IdBuilders.Extensions
{
    /// <summary>
    /// ID获取扩展方法
    /// </summary>
    internal static class IdGetterExtension
    {
        /// <summary>
        /// Key:Id类型
        /// </summary>
        public static readonly Dictionary<Type, Func<object>> IdFuncsForType
            = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// Key:Entity类型
        /// </summary>
        public static readonly Dictionary<Type, Func<object>> EntitiesIdFunc
            = new Dictionary<Type, Func<object>>();

        static IdGetterExtension()
        {
            IdFuncsForType[typeof(Guid)] = () => Guid.NewGuid();
        }

        public static TKey CreateIndentity<TKey>(this IEntity entity)
        {
            var entityType = entity.GetType();
            if (EntitiesIdFunc.ContainsKey(entityType))
                return (TKey)EntitiesIdFunc[entityType].Invoke();

            var keyType = typeof(TKey);
            if (IdFuncsForType.ContainsKey(keyType))
                return (TKey)IdFuncsForType[keyType].Invoke();
            else
                return default;
        }
    }
}
