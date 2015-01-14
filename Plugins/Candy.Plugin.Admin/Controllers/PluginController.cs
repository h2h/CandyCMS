using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candy.Core.Controllers;
using Candy.Framework.Plugins;

namespace Candy.Plugin.Admin.Controllers
{
    public class PluginController : BaseAdminController
    {
        public object Install(string name)
        {
            return "Installed Success";
        }
        public object Uninstall(string name)
        {
            return "Uninstall Success";
        }
    }
}
