using System;
using Autofac;
using Candy.Framework.Configuration;
using Candy.Framework.Data;
using Candy.Framework.Infrastructure.DependencyManagement;

namespace Candy.Framework.Mvc.Extensions
{
    public static class DependencyRegistrarExtensions
    {
        public static void RegisterPluginDataContext<T>(this IDependencyRegistrar dependencyRegistrar,
            ContainerBuilder builder,string contextName)
            where T : IDbContext
        {
            builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<CandyConfig>().ConnectionString }))
                .Named<IDbContext>(contextName)
                .InstancePerLifetimeScope();
            builder.Register(c => (T)Activator.CreateInstance(typeof(T), new object[] { c.Resolve<CandyConfig>().ConnectionString }))
                .InstancePerLifetimeScope();

        }
    }
}
