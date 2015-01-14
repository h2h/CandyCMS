using System;
using System.Linq;
using System.Web.Routing;
using Candy.Framework.Plugins;
using Candy.Framework.Infrastructure;
using System.Collections.Generic;

namespace Candy.Framework.Mvc.Routes
{
    public class RoutePublisher : IRoutePublisher
    {
        protected readonly ITypeFinder typeFinder;

        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }
        /// <summary>
        /// 根据类型查找插件描述信息
        /// </summary>
        /// <param name="providerType">Type</param>
        /// <returns></returns>
        protected virtual PluginDescriptor FindPlugin(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException("providerType");

            foreach (var plugin in PluginManager.ReferencedPlugins)
            {
                if (plugin.ReferencedAssembly == null)
                    continue;

                if (plugin.ReferencedAssembly.FullName == providerType.Assembly.FullName)
                    return plugin;
            }
            return null;
        }
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes"></param>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();

            foreach (var providerType in routeProviderTypes)
            {
                var plugin = FindPlugin(providerType);

                if (plugin != null && !plugin.Installed)
                    continue;

                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }

            routeProviders.ForEach(r => r.RegisterRoutes(routes));
        }
    }
}
