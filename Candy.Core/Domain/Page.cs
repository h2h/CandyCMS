using System;
using Candy.Framework;

namespace Candy.Core.Domain
{
    public class Page : BaseEntity
    {
        public Page()
        {
            this.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 页面别名
        /// </summary>
        public string Slug { get; set; }

        public int Views { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}