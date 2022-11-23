using System;
using System.Collections.Generic;
using System.Text;

namespace Tesla.Framework.Domain.Abstractions.IdBuilders
{
    /// <summary>
    /// ID生成器实现
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class IdBuilder<TKey, TAggregateRoot> : IIdBuilder<TKey, TAggregateRoot>
        where TAggregateRoot : Entity<TKey>
    {
        private readonly IGenerateProvider _generateProvider;

        // 支持的Id类型
        private readonly Dictionary<Type, Func<object>> keyDefaultFunc
            = new Dictionary<Type, Func<object>>
        {
            { typeof(string),() => Guid.NewGuid().ToString().Replace("-","") },
            { typeof(Guid),() => Guid.NewGuid()}
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="generateProvider"></param>
        public IdBuilder(IGenerateProvider generateProvider)
        {
            _generateProvider = generateProvider;
        }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public TKey BuildId()
        {
            var keyType = typeof(TKey);
            if (keyType == typeof(long))
                return (TKey)(object)_generateProvider.GenerateId();

            if (!keyDefaultFunc.ContainsKey(keyType))
                throw new ArgumentException($"{typeof(TAggregateRoot).Name}的IdBuilder不支持类型为:{keyType}的Id生成");

            return (TKey)keyDefaultFunc[keyType]();
        }
    }
}
