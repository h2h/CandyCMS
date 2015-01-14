using System.Globalization;
using System.Collections.Generic;

using Candy.Framework.Localization;

namespace Candy.Core.Services
{
    public partial interface ILanguageService
    {
        IEnumerable<Language> GetAllLanguages();
        IEnumerable<CultureInfo> GetAllCultureInfo();
        void DeleteLanguage(string culture);
        void SaveLanguage(string culture);
        Language Current();
        Language GetByCulture(string culture);
    }
}
