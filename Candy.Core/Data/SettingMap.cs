using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Data
{
    public partial class SettingMap : CandyEntityTypeConfiguration<Setting>, BaseEntityMap
    {
        public SettingMap()
        {
            this.ToTable("Setting");
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}