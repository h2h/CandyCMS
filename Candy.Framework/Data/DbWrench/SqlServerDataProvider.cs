using System.Data.Common;
using System.Data.SqlClient;

namespace Candy.Framework.Data.DbWrench
{
    public class SqlServerDataProvider : IDataProvider
    {
        public virtual void InitConnectionFactory()
        { }

        public virtual void InitDatabase()
        {
            SetDatabaseInitializer();
        }

        public virtual void SetDatabaseInitializer()
        {
            // 创建表
        }

        /// <summary>
        /// 是否支持存储过程
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get
            {
                return true;
            }
        }

        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}