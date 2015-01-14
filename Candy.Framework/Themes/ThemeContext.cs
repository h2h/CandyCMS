using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Framework.Themes
{
    public partial class ThemeContext : IThemeContext
    {
        private readonly IThemeProvider _themeProvider;

        private bool _themeIsCached;
        private string _cachedThemeName;
        public ThemeContext(IThemeProvider themeProvider)
        {
            this._themeProvider = themeProvider;
        }

        public string WorkingThemeName
        {
            get
            {
                if (_themeIsCached)
                    return _cachedThemeName;

                var theme = "Candy.Theme.Default";

                if (!_themeProvider.ThemeDescriptorExists(theme))
                {
                    var themeInstance = _themeProvider.GetThemeDescriptions()
                        .FirstOrDefault();

                    if (themeInstance == null)
                        throw new Exception("No Theme could be loaded");

                    theme = themeInstance.PackageName;
                }

                _cachedThemeName = theme;
                _themeIsCached = true;
                return theme;
            }
            set
            {
                this._themeIsCached = false;
            }
        }
    }
}
