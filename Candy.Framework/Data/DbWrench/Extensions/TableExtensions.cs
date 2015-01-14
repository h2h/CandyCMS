using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ivony.Data;
using Ivony.Data.Queries;

namespace Candy.Framework.Data.DbWrench.Extensions
{
    public static class TableExtensions
    {
        public static void Table(this IDbExecutor<ParameterizedQuery> execute, string tableName)
        {

        }
        public static void Table<T>(this IDbExecutor<ParameterizedQuery> execute)
        {

        }
    }
}
