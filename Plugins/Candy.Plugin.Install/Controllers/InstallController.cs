using System;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mvc;
using System.Xml;
using Candy.Core.Controllers;
using Candy.Framework.Data;
using Candy.Framework.Infrastructure;
using Candy.Plugin.Install.Models;
using Candy.Plugin.Install.Services;

namespace Candy.Plugin.Install.Controllers
{
    public class InstallController : BasePluginController
    {
        public ActionResult Index()
        {
            var model = new InstallViewModel();
            model.DbServer = "localhost";
            model.UserName = "admin";
            return View(model);
        }

        [HttpPost]
        public ActionResult Connection(InstallViewModel model)
        {
            if (!string.IsNullOrEmpty(model.DbName) &&
                !string.IsNullOrEmpty(model.DbServer) &&
                !string.IsNullOrEmpty(model.DbUser) &&
                !string.IsNullOrEmpty(model.DbPassword))
            {
                var connectionStr = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;Persist Security Info=False;User ID={2};Password={3}", model.DbServer, model.DbName, model.DbUser, model.DbPassword);

                if (!SqlServerDatabaseExists(connectionStr))
                {
                    var error = CreateDatabase(connectionStr);
                    if (!string.IsNullOrEmpty(error))
                        throw new Exception(error);

                    Thread.Sleep(3000);
                }
                // 写入连接配置
                WriteConnectionString(connectionStr, "SqlServer");

                var dataProviderInstance = EngineContext.Current.Resolve<BaseDataProviderManager>().LoadDataProvider();
                dataProviderInstance.InitDatabase();

                var installationService = EngineContext.Current.Resolve<IInstallationService>();
                installationService.InstallData(model.Email, model.Password, model.UserName);

                return Json(new
                {
                    Status = true
                });
            }
            else
            {
                return Json(new
                {
                    Status = false,
                    Message = ""
                });
            }
        }

        public object Reinstall()
        {
            var areaName = ControllerContext.RouteData.DataTokens["area"];
            return "Install.Reinstall , Area Name:" + areaName;
        }

        public ActionResult Upgrade()
        {
            return View();
        }

        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        protected bool SqlServerDatabaseExists(string connectionString)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected string CreateDatabase(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);

                var databaseName = builder.InitialCatalog;

                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("CREATE DATABASE [{0}]", databaseName);

                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected bool WriteConnectionString(string connectionString, string providerName)
        {
            try
            {
                string fileName = HttpContext.Server.MapPath("~/Web.config");

                System.IO.File.SetAttributes(fileName, System.IO.FileAttributes.Normal);

                XmlDocument configDoc = new XmlDocument();
                configDoc.Load(fileName);

                foreach (XmlElement ele in configDoc.DocumentElement.ChildNodes)
                {
                    if (!ele.Name.Equals("CandyConfig", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (ele.ChildNodes.Count <= 0)
                        continue;

                    foreach (XmlElement e in ele.ChildNodes)
                    {
                        switch (e.Name)
                        {
                            case "DataProvider":
                                e.Attributes["ProviderName"].Value = providerName;
                                e.Attributes["ConnectionString"].Value = connectionString;
                                break;

                            case "Application":
                                e.Attributes["IsInstalled"].Value = "true";
                                break;
                        }
                    }
                }

                configDoc.Save(fileName);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}