using System.Collections.Generic;

namespace Candy.Framework.Themes
{
    public interface IThemeProvider
    {
        ThemeDescriptor GetThemeDescriptor(string themePackage);

        IList<ThemeDescriptor> GetThemeDescriptions();

        bool ThemeDescriptorExists(string themePackage);
    }
}