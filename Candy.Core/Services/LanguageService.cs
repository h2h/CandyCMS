using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

using Candy.Core.Domain;

using Candy.Framework.Caching;
using Candy.Framework.Localization;

namespace Candy.Core.Services
{
    public partial class LanguageService : ILanguageService
    {
        private const string LANGUAGES_ALL_KEY = "Candy.Language.All";
        private const string LANGUAGES_CULTURE_KEY = "Candy.Language.{0}";

        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;

        public LanguageService(ICacheManager cacheManager,
            ISettingService settingService)
        {
            this._cacheManager = cacheManager;
            this._settingService = settingService;
        }
        public IEnumerable<Language> GetAllLanguages()
        {
            return LocalizerManager.Languages;
        }
        public IEnumerable<CultureInfo> GetAllCultureInfo()
        {
            var cultures = new List<CultureInfo>();
            var languages = GetAllLanguages();
            foreach (var language in languages)
            {
                cultures.Add(language.LanguageCulture);
            }
            return cultures;
        }
        public void DeleteLanguage(string culture)
        {
        }
        public void SaveLanguage(string culture)
        {
            
        }
        public Language Current()
        {
            var siteSetting = this._settingService.LoadSetting<SiteSettings>();
            
            string key = string.Format(LANGUAGES_CULTURE_KEY,siteSetting.Language);
            var language = this._cacheManager.Get(key, () => { return GetByCulture(siteSetting.Language); });

            return language;
        }
        public Language GetByCulture(string culture)
        {
            return LocalizerManager.Languages.Where(l => l.LanguageCulture.Name.Equals(culture,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
