using System.Collections.Generic;
using System.Linq;
using Candy.Framework.Data.DbWrench.Extensions;

using Ivony.Data.SqlClient;

namespace Candy.Framework.Data.DbWrench
{
    public partial class DbWrenchRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SqlDbExecutor db;
        private readonly IDbContext _context;

        public DbWrenchRepository(IDbContext context)
        {
            this._context = context;
        }

        public virtual T GetById(object id)
        {
            db.Table<T>();
            return null;
        }

        public virtual void Insert(T entity)
        { }

        public virtual void Insert(IEnumerable<T> entities)
        { }

        public virtual void Update(T entity)
        { }

        public virtual void Delete(T entity)
        { }

        public virtual void Delete(IEnumerable<T> entities)
        { }

        public virtual IQueryable<T> Table
        {
            get
            {
                return null;
            }
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return null;
            }
        }
    }
}