using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Candy.Framework.Themes
{
    public class ThemeDescriptor
    {
        public ThemeDescriptor()
        {
        }
        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主题文件夹名称
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// 主题描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 作者 Url
        /// </summary>
        public string AuthorUrl { get; set; }
        /// <summary>
        /// 主题版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 主题 Url
        /// </summary>
        public string ThemeUrl { get; set; }
        /// <summary>
        /// 主题标签
        /// </summary>
        public string Tags { get; set; }
    }
}