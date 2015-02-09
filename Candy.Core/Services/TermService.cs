using Candy.Core.Domain;

using Candy.Framework.Data;

namespace Candy.Core.Services
{
    public partial class TermService : ITermService
    {
        private readonly IRepository<Term> _termRepository;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="termRepository"></param>
        public TermService(IRepository<Term> termRepository)
        {
            this._termRepository = termRepository;
        }
        /// <summary>
        /// 插入到数据库
        /// </summary>
        /// <param name="model"></param>
        public void Create(Term entity)
        {
            this._termRepository.Insert(entity);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        public void Update(Term entity)
        {
            this._termRepository.Update(entity);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Term entity)
        {
            this._termRepository.Delete(entity);
        }
    }
}