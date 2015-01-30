using System;

using Candy.Framework;

namespace Candy.Core.Domain
{
    public partial class Article : BaseEntity
    {
        public Article()
        {
            this.CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文章别名
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// 文章查看次数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsSticky { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}