using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Tesla.Framework.Core
{
    /// <summary>
    /// 消息扩展
    /// </summary>
    public static class MessageExtension
    {
        /// <summary>
        /// 消息获取函数
        /// </summary>
        public static Func<Enum, string> MessageGetterFunc { private get; set; }

        /// <summary>
        /// 抛出异常消息
        /// </summary>
        /// <param name="em"></param>
        /// <param name="message"></param>
        /// <exception cref="MessageException"></exception>
        public static void ThrowMessage(this Enum em, string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = MessageGetterFunc?.Invoke(em);
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                FieldInfo field = em.GetType().GetField(em.ToString());
                if (field != null)
                {
                    DescriptionAttribute customAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                    if (customAttribute != null)
                    {
                        message = customAttribute.Description;
                    }
                }

                if (string.IsNullOrEmpty(message))
                {
                    message = em.ToString();
                }
            }

            throw new MessageException(message, em.GetHashCode());
        }
    }
}
