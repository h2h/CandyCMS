using System.Collections.Generic;
using Candy.Core.Domain;
namespace Candy.Core.Services
{
    public partial interface ITermTaxonomyService
    {
        void Create(TermTaxonomy model);
        void Update(TermTaxonomy entity);
        void Delete(TermTaxonomy entity);
        void Delete(int id);
        void Delete(IEnumerable<TermTaxonomy> entites);
        void Delete(int[] ids);
        IPagedList<TermTaxonomy> GetPagedTaxonomy(TaxonomyType taxonomy,
            string searchKey = null,
            int pageIndex = 0,
            int pageSize = 2147483647);
    }
}