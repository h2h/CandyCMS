using System;
using System.Collections.Generic;
using System.Linq;

using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Services
{
    public partial class TermTaxonomyService : ITermTaxonomyService
    {
        private readonly ITermService _termService;
        private readonly IRepository<TermTaxonomy> _taxonomyRepository;

        public TermTaxonomyService(ITermService termService, IRepository<TermTaxonomy> taxonomyRepository)
        {
            this._termService = termService;
            this._taxonomyRepository = taxonomyRepository;
        }

        public void Create(TermTaxonomy entity)
        {
            this._termService.Create(entity.Term);

            entity.TermId = entity.Term.Id;

            this._taxonomyRepository.Insert(entity);
        }

        public void Update(TermTaxonomy entity)
        {
            this._taxonomyRepository.Update(entity);
        }

        public void Delete(TermTaxonomy entity)
        {
            this._taxonomyRepository.Delete(entity);
        }

        public void Delete(int id)
        {
            var entity = this._taxonomyRepository.GetById(id);
            this.Delete(entity);
        }

        public void Delete(IEnumerable<TermTaxonomy> entites)
        {
            this._taxonomyRepository.Delete(entites);
        }

        public void Delete(int[] ids)
        {
            foreach (var id in ids)
                this.Delete(id);
        }

        public virtual IPagedList<TermTaxonomy> GetPagedTaxonomy(TaxonomyType taxonomy,
            string searchKey = null,
            int pageIndex = 0,
            int pageSize = 2147483647)
        {
            var query = this._taxonomyRepository.Table;

            query = query.Where(a => a.Taxonomy.Equals(taxonomy.ToString(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(searchKey))
                query = query.Where(a => a.Term.Name.Contains(searchKey) || a.Term.Slug.Contains(searchKey));

            query = query.OrderByDescending(a => a.Id);

            return new PagedList<TermTaxonomy>(query, pageIndex, pageSize);
        }
    }
}