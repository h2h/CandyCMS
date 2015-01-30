using System.Collections.Generic;
using System.Globalization;
using Candy.Core.Domain;

namespace Candy.Plugin.Admin.Domain
{
    public class SiteSettingModel
    {
        public SiteSettingModel()
        {
            Cultures = new List<CultureInfo>();
        }

        public SiteSettings SiteSettings { get; set; }

        public IList<CultureInfo> Cultures { get; set; }
    }
}