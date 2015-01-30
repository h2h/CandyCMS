using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Candy.Framework;
using Candy.Framework.Data;

namespace Candy.Plugin.Install.Services
{
    public partial class InstallationService : IInstallationService
    {
        private readonly IDbContext _dbContext;
        private readonly IWebHelper _webHelper;

        public InstallationService(IDbContext dbContext,
            IWebHelper webHelper)
        {
            this._dbContext = dbContext;
            this._webHelper = webHelper;
        }

        protected virtual void ExecuteSqlFile(string path)
        {
            var statements = new List<string>();

            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                    statements.Add(statement);
            }

            foreach (var stmt in statements)
                _dbContext.ExecuteSqlCommand(stmt);
        }

        protected virtual string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();
                    return null;
                }

                if (lineOfText.Trim().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }
            return sb.ToString();
        }

        protected virtual void UpdateDefaultUser(string email, string password, string username)
        {

        }

        public virtual void InstallData(string email, string password, string username, bool installSampleData = true)
        {
            // 添加必要配置数据
            ExecuteSqlFile(_webHelper.MapPath("~/App_Data/Install/Create.Required.Data.sql"));
            // 添加默认用户
            UpdateDefaultUser(email, password, username);
            // 添加演示数据
            if (installSampleData)
                ExecuteSqlFile(_webHelper.MapPath("~/App_Data/Install/Create.Sample.Data.sql"));
        }
    }
}