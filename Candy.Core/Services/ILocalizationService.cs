using Candy.Framework.Localization;

namespace Candy.Core.Services
{
    public partial interface ILocalizationService
    {
        LanguageResource GetByKey(string key);
    }
}