using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Candy.Framework.Configuration;
using Candy.Framework.Infrastructure;

namespace Candy.Framework.Data.EF
{
    public class CandyObjectContext : DbContext, IDbContext
    {
        public CandyObjectContext(CandyConfig config)
            : base(config.ConnectionString)
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            var typesToRegister = typeFinder.FindClassesOfType<BaseEntityMap>();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeOut = null;
            if (timeout.HasValue)
            {
                previousTimeOut = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction ? TransactionalBehavior.DoNotEnsureTransaction : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeOut;
            }

            return result;
        }
    }
}