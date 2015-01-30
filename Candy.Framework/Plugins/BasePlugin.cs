namespace Candy.Framework.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        protected BasePlugin()
        {
        }

        public virtual PluginDescriptor PluginDescriptor { get; set; }

        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.FileName);
        }

        public virtual void Uninstall()
        {
        }
    }
}