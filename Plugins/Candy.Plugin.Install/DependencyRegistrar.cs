using Autofac;

using Candy.Plugin.Install.Services;

using Candy.Framework.Data.EF;
using Candy.Framework.Infrastructure;
using Candy.Framework.Infrastructure.DependencyManagement;

namespace Candy.Plugin.Install
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<InstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
        }
    }
}