using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Globalization;
using System.Collections.Generic;

using Candy.Framework.Plugins;
using Candy.Framework.Localization;
using Candy.Framework.ComponentModel;

using Newtonsoft.Json;

namespace Candy.Framework.Localization
{
    public class LocalizerManager
    {
        private const string LanguagePath = "~/Languages/";
        private const string PluginsPath = "~/Plugins/";

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        /// <summary>
        /// 获取当前已加载语言包
        /// </summary>
        public static IEnumerable<Language> Languages { get; set; }
        public static void Initialize()
        {
            using (new WriteLockDisposable(Locker))
            {
                var languageFolder = new DirectoryInfo(HostingEnvironment.MapPath(LanguagePath));
                var loadedLanguages = new List<Language>();

                foreach (var lang in languageFolder.GetFiles("*.json", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var culture = CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(lang.FullName));
                        var text = File.ReadAllText(lang.FullName);
                        var language = JsonConvert.DeserializeObject<Language>(text);
                        loadedLanguages.Add(language);
                    }
                    catch
                    {
                        continue;
                    }
                }

                foreach (var plugin in PluginManager.ReferencedPlugins)
                {
                    var pluginLanguagePath = Path.Combine(Path.GetDirectoryName(plugin.OriginalAssemblyFile.FullName), "Languages");
                    if (!Directory.Exists(pluginLanguagePath))
                        continue;

                    var pluginLanguageFolder = new DirectoryInfo(pluginLanguagePath);
                    foreach (var lang in pluginLanguageFolder.GetFiles("*.json", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            var culture = CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(lang.FullName));
                            var LanguageCulture = Path.GetFileNameWithoutExtension(lang.FullName);
                            var text = File.ReadAllText(lang.FullName);
                            var language = JsonConvert.DeserializeObject<Language>(text);

                            if (loadedLanguages.Any(l => l.LanguageCulture == language.LanguageCulture))
                            {
                                loadedLanguages.ForEach(l => {
                                    if (l.LanguageCulture == language.LanguageCulture)
                                        l.InsertResources(language.LanguageResources.ToList());
                                });
                            }
                            else
                                loadedLanguages.Add(language);

                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                Languages = loadedLanguages;
            }
        }
    }
}
