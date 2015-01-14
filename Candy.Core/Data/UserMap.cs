using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Data
{
    public partial class UserMap : CandyEntityTypeConfiguration<User>, BaseEntityMap
    {
        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(a => a.Id);
        }
    }
}
