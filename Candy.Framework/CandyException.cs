using System;

namespace Candy.Framework
{
    /// <summary>
    /// 错误信息
    /// </summary>
    [Serializable]
    public class CandyException : Exception
    {
        /// <summary>
        /// 初始化实例
        /// </summary>
        public CandyException()
        {
        }

        /// <summary>
        /// 初始化实例并指定错误消息
        /// </summary>
        /// <param name="message">消息描述</param>
        public CandyException(string message)
            : base(message)
        {
        }

        public CandyException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }
    }
}