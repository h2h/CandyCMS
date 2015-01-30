using Candy.Framework.Configuration;

namespace Candy.Core.Domain
{
    public class SiteSettings : ISettings
    {
        /// <summary>
        /// 网站是否开启
        /// </summary>
        public bool Enbaled { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 网站副标题
        /// </summary>
        public string SiteDescription { get; set; }

        /// <summary>
        /// 网站网址
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// 网站备案号
        /// </summary>
        public string ICPNo { get; set; }

        /// <summary>
        /// 网站关键字
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat { get; set; }

        /// <summary>
        /// 管理员邮箱地址
        /// </summary>
        public string AdminEmailAddress { get; set; }

        /// <summary>
        /// 消息邮箱地址
        /// </summary>
        public string NotificationEmailAddress { get; set; }

        /// <summary>
        /// SMTP 服务器
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        /// SMTP 端口号
        /// </summary>
        public string SMTPProt { get; set; }

        /// <summary>
        /// SMTP 用户名
        /// </summary>
        public string SMTPUserName { get; set; }

        /// <summary>
        /// SMTP 密码
        /// </summary>
        public string SMTPPassword { get; set; }
    }
}