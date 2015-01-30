using System;
using System.Configuration;
using System.Xml;

namespace Candy.Framework.Configuration
{
    public partial class CandyConfig : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new CandyConfig();

            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }

            var startupNode = section.SelectSingleNode("Startup");
            if (startupNode != null && startupNode.Attributes != null)
            {
                var ignoreStartupTasks = startupNode.Attributes["IgnoreStartupTasks"];
                if (ignoreStartupTasks != null)
                    config.IgnoreStartupTasks = Convert.ToBoolean(ignoreStartupTasks.Value);
            }

            var applicationNode = section.SelectSingleNode("Application");
            if (applicationNode != null && applicationNode.Attributes != null)
            {
                var isInstalled = applicationNode.Attributes["IsInstalled"];
                if (isInstalled != null)
                    config.IsInstalled = Convert.ToBoolean(isInstalled.Value);

                var version = applicationNode.Attributes["Version"];
                if (version != null)
                    config.Version = version.Value;
            }

            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }

            var dataProviderNode = section.SelectSingleNode("DataProvider");
            if (dataProviderNode != null && dataProviderNode.Attributes != null)
            {
                var providerName = dataProviderNode.Attributes["ProviderName"];
                if (providerName != null)
                    config.DataProviderName = providerName.Value;

                var connectionString = dataProviderNode.Attributes["ConnectionString"];
                if (connectionString != null)
                    config.ConnectionString = connectionString.Value;
            }

            return config;
        }

        public string EngineType { get; private set; }

        public bool DynamicDiscovery { get; private set; }

        public bool IgnoreStartupTasks { get; private set; }

        public string ThemeBasePath { get; private set; }

        public bool IsInstalled { get; private set; }

        public string Version { get; private set; }

        public string DataProviderName { get; private set; }

        public string ConnectionString { get; private set; }
    }
}