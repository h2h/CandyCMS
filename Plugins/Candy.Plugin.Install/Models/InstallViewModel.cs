using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Candy.Plugin.Install.Models
{
    public class InstallViewModel
    {
        /// <summary>
        /// 管理员帐号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 时区
        /// </summary>
        public string TimeZone { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string DbServer { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string DbUser { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DbPassword { get; set; }
    }
}