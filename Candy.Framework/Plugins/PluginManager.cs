using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Compilation;
using System.Collections.Generic;

using Newtonsoft.Json;

using Candy.Framework.Plugins;
using Candy.Framework.ComponentModel;

[assembly: PreApplicationStartMethod(typeof(PluginManager), "Initialize")]
namespace Candy.Framework.Plugins
{
    public class PluginManager
    {
        private const string InstalledPluginsFilePath = "~/App_Data/InstalledPlugins.json";
        private const string PluginsPath = "~/Plugins";
        private const string ShadowCopyPath = "~/Plugins/bin";

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static DirectoryInfo _shadowCopyFolder;

        /// <summary>
        /// 返回所有已引用的插件集合
        /// </summary>
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }

        /// <summary>
        /// 返回所有与当前程序版本兼容的插件集合
        /// </summary>
        public static IEnumerable<string> IncompatiblePlugins { get; set; }

        /// <summary>
        /// 插件管理器初始化
        /// </summary>
        public static void Initialize()
        {
            using (new WriteLockDisposable(Locker))
            {
                var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath(PluginsPath));
                _shadowCopyFolder = new DirectoryInfo(HostingEnvironment.MapPath(ShadowCopyPath));

                var referencedPlugins = new List<PluginDescriptor>();
                var incompatiblePlugins = new List<string>();

                try
                {
                    var installedPluginFileNames = PluginFileParser.ParseInstalledPluginsFile(GetInstalledPluginsFilePath());

                    Directory.CreateDirectory(pluginFolder.FullName);
                    Directory.CreateDirectory(_shadowCopyFolder.FullName);

                    var binFiles = _shadowCopyFolder.GetFiles("*", SearchOption.AllDirectories);

                    foreach (var dfd in GetDescriptionFilesAndDescriptors(pluginFolder))
                    {
                        var descriptionFile = dfd.Key;
                        var pluginDescriptor = dfd.Value;

                        if (string.IsNullOrWhiteSpace(pluginDescriptor.Name))
                            throw new Exception(string.Format("A plugin '{0}' has no system name. Try assigning the plugin a unique name and recompiling.", descriptionFile.FullName));
                        
                        if (referencedPlugins.Contains(pluginDescriptor))
                            throw new Exception(string.Format("A plugin with '{0}' system name is already defined", pluginDescriptor.Name));

                        pluginDescriptor.Installed = installedPluginFileNames
                            .FirstOrDefault(x => x.Equals(pluginDescriptor.FileName, StringComparison.InvariantCultureIgnoreCase)) != null;

                        try
                        {
                            if (descriptionFile.Directory == null)
                                throw new Exception(string.Format("Directory cannot be resolved for '{0}' description file", descriptionFile.Name));

                            var pluginFiles = descriptionFile.Directory.GetFiles("*.dll", SearchOption.AllDirectories)
                                .Where(x => !binFiles.Select(q => q.FullName).Contains(x.FullName))
                                .Where(x => IsPackagePluginFolder(x.Directory))
                                .ToList();

                            var mainPluginFile = pluginFiles
                                .FirstOrDefault(x => x.Name.Equals(pluginDescriptor.FileName, StringComparison.InvariantCultureIgnoreCase));
                            pluginDescriptor.OriginalAssemblyFile = mainPluginFile;

                            pluginDescriptor.ReferencedAssembly = PerformFileDeploy(mainPluginFile);

                            // 加载所有未加载插件
                            foreach (var plugin in pluginFiles
                                .Where(x => !x.Name.Equals(mainPluginFile.Name, StringComparison.InvariantCultureIgnoreCase))
                                .Where(x => !IsAlreadyLoaded(x)))
                            {
                                PerformFileDeploy(plugin);
                            }

                            foreach (var t in pluginDescriptor.ReferencedAssembly.GetTypes())
                            {
                                if (typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && t.IsClass && !t.IsAbstract)
                                {
                                    pluginDescriptor.PluginType = t;
                                    break;
                                }
                            }

                            referencedPlugins.Add(pluginDescriptor);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                catch
                {
                    throw;
                }

                ReferencedPlugins = referencedPlugins;
                IncompatiblePlugins = incompatiblePlugins;
            }
        }
        /// <summary>
        /// 获取插件描述信息
        /// </summary>
        /// <param name="pluginFolder">插件文件夹</param>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<FileInfo, PluginDescriptor>> GetDescriptionFilesAndDescriptors(DirectoryInfo pluginFolder)
        {
            if (pluginFolder == null)
                throw new ArgumentNullException("pluginFolder");

            var result = new List<KeyValuePair<FileInfo, PluginDescriptor>>();

            foreach (var descriptionFile in pluginFolder.GetFiles("Plugin.json", SearchOption.AllDirectories))
            {
                if (!IsPackagePluginFolder(descriptionFile.Directory))
                    continue;

                var pluginDescription = PluginFileParser.ParsePluginDescriptionFile(descriptionFile.FullName);

                result.Add(new KeyValuePair<FileInfo, PluginDescriptor>(descriptionFile, pluginDescription));
            }
            // 根据 Display Order 排序
            result.Sort((firstPair, nextPair) => firstPair.Value.DisplayOrder.CompareTo(nextPair.Value.DisplayOrder));
            return result;
        }
        /// <summary>
        /// 确定该文件夹是否在插件文件夹下
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private static bool IsPackagePluginFolder(DirectoryInfo folder)
        {
            if (folder == null) return false;
            if (folder.Parent == null) return false;
            if (!folder.Parent.Name.Equals("Plugins", StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }
        /// <summary>
        /// 获取已安装插件列表保存路径
        /// </summary>
        /// <returns></returns>
        private static string GetInstalledPluginsFilePath()
        {
            return HostingEnvironment.MapPath(InstalledPluginsFilePath);
        }
        /// <summary>
        /// 插件文件是否已经加载
        /// </summary>
        /// <param name="fileInfo">插件文件</param>
        /// <returns>Result</returns>
        private static bool IsAlreadyLoaded(FileInfo fileInfo)
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                if (fileNameWithoutExt == null)
                    throw new Exception(string.Format("Cannot get file extnension for {0}", fileInfo.Name));
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    string assemblyName = a.FullName.Split(new[] { ',' }).FirstOrDefault();
                    if (fileNameWithoutExt.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }
        /// <summary>
        /// 插件文件部署
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <returns></returns>
        private static Assembly PerformFileDeploy(FileInfo plugin)
        {
            if (plugin.Directory.Parent == null)
                throw new InvalidOperationException("The plugin directory for the " + plugin.Name +
                                                    " file exists in a folder outside of the allowed nopCommerce folder heirarchy");

            FileInfo shadowCopiedPlug;

            if (CommonHelper.GetTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)
            {
                var shadowCopyPlugFolder = Directory.CreateDirectory(_shadowCopyFolder.FullName);
                shadowCopiedPlug = InitializeMediumTrust(plugin, shadowCopyPlugFolder);
            }
            else
            {
                var directory = AppDomain.CurrentDomain.DynamicDirectory;
                shadowCopiedPlug = InitializeFullTrust(plugin, new DirectoryInfo(directory));
            }

            //加载插件
            var shadowCopiedAssembly = Assembly.Load(AssemblyName.GetAssemblyName(shadowCopiedPlug.FullName));

            //添加到 Build Manager
            BuildManager.AddReferencedAssembly(shadowCopiedAssembly);

            return shadowCopiedAssembly;
        }
        /// <summary>
        /// 初始化插件 Full Trust
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="shadowCopyPluginFolder"></param>
        /// <returns></returns>
        private static FileInfo InitializeFullTrust(FileInfo plugin, DirectoryInfo shadowCopyPluginFolder)
        {
            var shadowCopiedPlugin = new FileInfo(Path.Combine(shadowCopyPluginFolder.FullName, plugin.Name));
            try
            {
                File.Copy(plugin.FullName, shadowCopiedPlugin.FullName, true);
            }
            catch
            {
                try
                {
                    var oldFile = shadowCopiedPlugin.FullName + Guid.NewGuid().ToString("N") + ".old";
                    File.Move(shadowCopiedPlugin.FullName, oldFile);
                }
                catch (IOException exc)
                {
                    throw new IOException(shadowCopiedPlugin.FullName + " rename failed, cannot initialize plugin", exc);
                }
                File.Copy(plugin.FullName, shadowCopiedPlugin.FullName, true);
            }
            return shadowCopiedPlugin;
        }
        /// <summary>
        /// 初始化插件 Medium Trust
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="shadowCopyPlugFolder"></param>
        /// <returns></returns>
        private static FileInfo InitializeMediumTrust(FileInfo plugin, DirectoryInfo shadowCopyPluginFolder)
        {
            var shouldCopy = true;
            var shadowCopiedPlugin = new FileInfo(Path.Combine(shadowCopyPluginFolder.FullName, plugin.Name));

            if (shadowCopiedPlugin.Exists)
            {
                var areFilesIdentical = shadowCopiedPlugin.CreationTimeUtc.Ticks >= plugin.CreationTimeUtc.Ticks;
                if (areFilesIdentical)
                {
                    shouldCopy = false;
                }
                else
                {
                    File.Delete(shadowCopiedPlugin.FullName);
                }
            }

            if (shouldCopy)
            {
                try
                {
                    File.Copy(plugin.FullName, shadowCopiedPlugin.FullName, true);
                }
                catch (IOException)
                {
                    try
                    {
                        var oldFile = shadowCopiedPlugin.FullName + Guid.NewGuid().ToString("N") + ".old";
                        File.Move(shadowCopiedPlugin.FullName, oldFile);
                    }
                    catch (IOException exc)
                    {
                        throw new IOException(shadowCopiedPlugin.FullName + " rename failed, cannot initialize plugin", exc);
                    }

                    File.Copy(plugin.FullName, shadowCopiedPlugin.FullName, true);
                }
            }
            return shadowCopiedPlugin;
        }
        /// <summary>
        /// 标记已安装的插件
        /// </summary>
        /// <param name="pluginFileName">插件文件名</param>
        public static void MarkPluginAsInstalled(string pluginFileName)
        {
            if (string.IsNullOrEmpty(pluginFileName))
                throw new ArgumentNullException("pluginFileName");

            var filePath = HostingEnvironment.MapPath(InstalledPluginsFilePath);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            var installedPluginSystemNames = PluginFileParser.ParseInstalledPluginsFile(GetInstalledPluginsFilePath());
            bool alreadyMarkedAsInstalled = installedPluginSystemNames
                                .FirstOrDefault(x => x.Equals(pluginFileName, StringComparison.InvariantCultureIgnoreCase)) != null;
            
            if (!alreadyMarkedAsInstalled)
                installedPluginSystemNames.Add(pluginFileName);

            PluginFileParser.SaveInstalledPluginsFile(installedPluginSystemNames, filePath);
        }
        /// <summary>
        /// 标记已卸载的插件
        /// </summary>
        /// <param name="pluginFileName">插件文件名</param>
        public static void MarkPluginAsUninstalled(string pluginFileName)
        {
            if (string.IsNullOrEmpty(pluginFileName))
                throw new ArgumentNullException("pluginFileName");

            // 已安装插件名单保存地址
            var filePath = GetInstalledPluginsFilePath();

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            var installedPluginFileNames = PluginFileParser.ParseInstalledPluginsFile(filePath);
            // 插件是否存在已安装插件列表
            var alreadyMarkedAsInstalled = installedPluginFileNames
                .FirstOrDefault(x => x.Equals(pluginFileName, StringComparison.InvariantCultureIgnoreCase)) != null;

            if (alreadyMarkedAsInstalled)
                installedPluginFileNames.Remove(pluginFileName);

            // 保存更新后的插件列表名单
            PluginFileParser.SaveInstalledPluginsFile(installedPluginFileNames, filePath);
        }
    }
}
