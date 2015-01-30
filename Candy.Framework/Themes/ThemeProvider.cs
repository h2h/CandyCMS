using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Candy.Framework.Configuration;
using Newtonsoft.Json;

namespace Candy.Framework.Themes
{
    public partial class ThemeProvider : IThemeProvider
    {
        private readonly IList<ThemeDescriptor> _themeDescriptions = new List<ThemeDescriptor>();
        private readonly string _basePath = string.Empty;

        public ThemeProvider(CandyConfig config, IWebHelper webHelper)
        {
            _basePath = webHelper.MapPath(config.ThemeBasePath);
            LoadDescriptions();
        }

        private void LoadDescriptions()
        {
            //TODO:Use IFileStorage?
            foreach (string themeName in Directory.GetDirectories(_basePath))
            {
                var configuration = CreateThemeDescriptor(themeName);
                if (configuration != null)
                {
                    _themeDescriptions.Add(configuration);
                }
            }
        }

        private ThemeDescriptor CreateThemeDescriptor(string themePath)
        {
            var themeConfigFile = new FileInfo(Path.Combine(themePath, "Theme.json"));

            if (themeConfigFile.Exists)
            {
                return JsonConvert.DeserializeObject<ThemeDescriptor>(File.ReadAllText(themeConfigFile.FullName));
            }

            return null;
        }

        public ThemeDescriptor GetThemeDescriptor(string themePackage)
        {
            return _themeDescriptions.SingleOrDefault(x => x.PackageName.Equals(themePackage, StringComparison.InvariantCultureIgnoreCase));
        }

        public IList<ThemeDescriptor> GetThemeDescriptions()
        {
            return _themeDescriptions;
        }

        public bool ThemeDescriptorExists(string themePackage)
        {
            return _themeDescriptions.Any(x => x.PackageName.Equals(themePackage, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}