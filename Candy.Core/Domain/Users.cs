using System;
using Candy.Framework;

namespace Candy.Core.Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            this.CreateDate = DateTime.Now;
            this.LastLoginDate = DateTime.Now;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NiceName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        
        public string PasswordSalt { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }
    }
}
