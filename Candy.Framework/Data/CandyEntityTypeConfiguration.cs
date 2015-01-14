using System.Data.Entity.ModelConfiguration;

namespace Candy.Framework.Data
{
    public abstract class CandyEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        public CandyEntityTypeConfiguration()
        {
            PostInitialize();
        }
        protected virtual void PostInitialize()
        { }
    }
}
