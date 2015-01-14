using System.Collections.Generic;
using System.Globalization;

namespace Candy.Framework.Localization
{
    public partial class Language : BaseEntity
    {
        private ICollection<LanguageResource> _localeStringResources;
        public Language()
        { }

        public CultureInfo LanguageCulture { get; set; }

        public virtual ICollection<LanguageResource> LanguageResources
        {
            get
            {
                return _localeStringResources ?? (_localeStringResources = new List<LanguageResource>());
            }
            protected set
            {
                _localeStringResources = value;
            }
        }
        public virtual void InsertResources(LanguageResource resource)
        {
            this._localeStringResources.Add(resource);
        }
        public virtual void InsertResources(IList<LanguageResource> resource)
        {
            foreach (var r in resource)
                this._localeStringResources.Add(r);
        }
    }
}
