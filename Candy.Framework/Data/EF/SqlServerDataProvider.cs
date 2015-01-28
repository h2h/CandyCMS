using System;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Web.Hosting;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Candy.Framework.Data.EF
{
    public class SqlServerDataProvider : IDataProvider
    {
        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException(string.Format("Specified file doesn't exist - {0}", filePath));

                return new string[0];
            }


            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
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

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlConnectionFactory();

            #pragma warning disable 0618
            Database.DefaultConnectionFactory = connectionFactory;
        }
        public virtual void SetDatabaseInitializer()
        {
            var tablesToValidate = new[] { "Settings" };
            var customCommands = new List<string>();
            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            customCommands.AddRange(ParseCommands(HostingEnvironment.MapPath("~/App_Data/Install/SqlServer.Indexes.sql"), false));
            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            customCommands.AddRange(ParseCommands(HostingEnvironment.MapPath("~/App_Data/Install/SqlServer.StoredProcedures.sql"), false));

            var initializer = new CreateTablesIfNotExist<CandyObjectContext>(tablesToValidate, customCommands.ToArray());
            Database.SetInitializer(initializer);

        }
        public virtual DbParameter GetParameter()
        {
            return null;
        }
        public virtual bool StoredProceduredSupported
        {
            get
            {
                return true;
            }
        }
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

    }
}
