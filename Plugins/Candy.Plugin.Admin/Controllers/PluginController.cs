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