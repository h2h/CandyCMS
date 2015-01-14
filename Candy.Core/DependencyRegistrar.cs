using Autofac;
using Autofac.Core;

using Candy.Core.Data;
using Candy.Core.Services;

using Candy.Framework.Mvc.Extensions;
using Candy.Framework.Caching;
using Candy.Framework.Infrastructure;
using Candy.Framework.Infrastructure.DependencyManagement;

namespace Candy.Core
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {   
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("candy_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();


            //builder.RegisterSource(new SettingsSource());
        }
    }
}
