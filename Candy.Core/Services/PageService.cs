using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Services
{
    public partial class PageService
    {
        private readonly IRepository<Page> _pageRepository;

        public PageService(IRepository<Page> pageRepository)
        {
            this._pageRepository = pageRepository;
        }

        public Page Get(int id)
        {
            return this._pageRepository.GetById(id);
        }

        public void Create(Page entity)
        {
        }

        public void Delete(Page entity)
        {
            this._pageRepository.Delete(entity);
        }

        public void Delete(int id)
        {
            this._pageRepository.Delete(Get(id));
        }
    }
}