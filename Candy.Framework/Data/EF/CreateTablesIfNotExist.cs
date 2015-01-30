using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;

namespace Candy.Framework.Data.EF
{
    public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        private readonly string[] _tablesToValidate;
        private readonly string[] _customCommands;

        public CreateTablesIfNotExist(string[] tablesToValidate, string[] customCommands)
        {
            this._tablesToValidate = tablesToValidate;
            this._customCommands = customCommands;
        }

        public void InitializeDatabase(TContext context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }

            if (!dbExists)
                throw new ApplicationException("No database instance");

            bool createTables;
            if (_tablesToValidate != null && _tablesToValidate.Length > 0)
            {
                var existingTableNames = new List<string>(context.Database.SqlQuery<string>("SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'"));
                createTables = !existingTableNames.Intersect(_tablesToValidate, StringComparer.OrdinalIgnoreCase).Any();
            }
            else
            {
                int numberOfTables = 0;
                foreach (var t1 in context.Database.SqlQuery<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE' "))
                    numberOfTables = t1;
                createTables = numberOfTables == 0;
            }

            if (createTables)
            {
                var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                context.Database.ExecuteSqlCommand(dbCreationScript);

                context.SaveChanges();

                if (_customCommands != null && _customCommands.Length > 0)
                {
                    foreach (var command in _customCommands)
                        context.Database.ExecuteSqlCommand(command);
                }
            }
        }
    }
}