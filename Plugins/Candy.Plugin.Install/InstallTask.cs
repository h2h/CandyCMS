using System;
using System.Web;
using System.Web.Routing;
using Candy.Core.Services;
using Candy.Framework.Configuration;
using Candy.Framework.Infrastructure;
using Candy.Framework.Mvc.Extensions;

namespace Candy.Plugin.Install
{
    public class InstallBeginRequestTask : IBeginRequestTask
    {
        public void Execute()
        {
            /*
             * 启动程序的时候，检查程序是否已经安装。
             * 如果已安装，检查程序版本号是否跟数据库中的程序版本号相同
             * 如果不相同，则进入升级页面
             * 如果未安装，则进入安装页面
             * 7000*120
             */

            var areaName = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).GetAreaName();

            if (string.IsNullOrEmpty(areaName) || !areaName.Equals(RouteProvider.AreaName, StringComparison.OrdinalIgnoreCase))
            {
                var config = EngineContext.Current.Resolve<CandyConfig>();
                if (config.IsInstalled)
                {
                    var settingsService = EngineContext.Current.Resolve<ISettingService>();
                    var version = settingsService.GetSetting("system.version");
                    // 检查数据库版本是否跟程序版本一致
                    if (version != null && !version.Value.Trim().Equals(config.Version, StringComparison.OrdinalIgnoreCase))
                    {
                        HttpContext.Current.Response.Redirect("/Upgrade/");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Install/");
                }
            }
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }
    }
}