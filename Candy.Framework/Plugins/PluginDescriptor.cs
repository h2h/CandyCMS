using System;
using System.IO;
using System.Reflection;

namespace Candy.Framework.Plugins
{
    public class PluginDescriptor : IComparable<PluginDescriptor>
    {
        public PluginDescriptor()
        {
        }

        public virtual Assembly ReferencedAssembly { get; internal set; }

        public virtual FileInfo OriginalAssemblyFile { get; internal set; }

        public virtual Type PluginType { get; set; }

        public virtual string Name { get; set; }

        public virtual string FileName { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual string Category { get; set; }

        public virtual string Description { get; set; }

        public virtual string SupportedVersions { get; set; }

        public virtual string Version { get; set; }

        public virtual string Website { get; set; }

        public virtual string Author { get; set; }

        /// <summary>
        /// 获取或设置该值指定插件是否安装
        /// </summary>
        public virtual bool Installed { get; set; }

        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);

            return Name.CompareTo(other.Name);
        }
    }
}