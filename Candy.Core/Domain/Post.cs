using System;

using Candy.Framework;

namespace Candy.Core.Domain
{
    public partial class Post : BaseEntity
    {
        public Post()
        {
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string PostContent { get; set; }

        /// <summary>
        /// IP 地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime EditedDate { get; set; }
    }
}