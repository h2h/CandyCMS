using System;
namespace Candy.Framework.Themes
{
    public interface IThemeContext
    {
        /// <summary>
        /// 当前使用主题名称
        /// </summary>
        string WorkingThemeName { get; set; }
    }
}
