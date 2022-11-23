using System;
using System.ComponentModel;
using System.Reflection;
using Tesla.Framework.Core.Messages;

namespace Tesla.Framework.Core.Extensions
{
    /// <summary>
    /// 异常消息扩展
    /// </summary>
    public static class ExceptionMsgExtension
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

        /// <summary>
        /// 抛异常消息
        /// </summary>
        /// <param name="em"></param>
        /// <param name="message"></param>
        /// <param name="errorData"></param>
        /// <exception cref="MessageException"></exception>
        public static void ThrowLanMessage(this Enum em, string message = "", object[] errorData = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = em.LDes();
            }

            throw new MessageException(message, em.GetHashCode(), errorData);
        }

        /// <summary>
        /// 抛异常消息(带填充参数)
        /// </summary>
        /// <param name="em"></param>
        /// <param name="obj"></param>
        public static void ThrowLanMessageParams(this Enum em, params object[] obj)
        {
            string text = em.LDes();
            if (obj != null && obj.Length != 0 && text.IndexOf("{0}") >= 0)
            {
                text = string.Format(text, obj);
            }

            em.ThrowMessage(text);
        }

        /// <summary>
        /// 消息本地化
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string LDes(this Enum em)
        {
            DescriptionAttribute attribute = em.GetAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }

            return em.ToString();
        }

        /// <summary>
        /// 获取自定义属性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="em"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum em) where TAttribute : Attribute
        {
            FieldInfo field = em.GetType().GetField(em.ToString());
            if (field == null)
            {
                return null;
            }

            return field.GetCustomAttribute<TAttribute>();
        }
    }
}
