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
        public void Create(Term model)
        {
            this._termRepository.Insert(model);
        }
    }
}