using System.Collections.Generic;
using System.Linq;

namespace Candy.Framework.Data
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);

        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 删除一个对象集合
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// 获取一个表
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 获取一个表，不启用 EF 跟踪，只能进行读取操作
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}