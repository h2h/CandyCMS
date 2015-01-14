using Candy.Framework;

namespace Candy.Core.Domain
{
    public partial class Setting : BaseEntity
    {
        public Setting()
        { }

        public Setting(string name, string value, bool autoLoad = false)
        {
            this.Name = name;
            this.Value = value;
            this.AutoLoad = autoLoad;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool AutoLoad { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
