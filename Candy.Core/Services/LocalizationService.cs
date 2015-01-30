using System;
using System.Linq;

using Candy.Framework.Localization;

namespace Candy.Core.Services
{
    public partial class LocalizationService : ILocalizationService
    {
        private readonly ILanguageService _languageService;

        public LocalizationService(ILanguageService languageService)
        {
            this._languageService = languageService;
        }

        public LanguageResource GetByKey(string key)
        {
            // 缓存中只存当前选中的语言
            return this._languageService.Current().LanguageResources
                .Where(l => l.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}