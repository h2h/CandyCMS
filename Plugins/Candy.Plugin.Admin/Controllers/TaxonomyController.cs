using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Candy.Core.Domain;
using Candy.Core.Services;
using Candy.Plugin.Admin.Domain;

namespace Candy.Plugin.Admin.Controllers
{
    public class TaxonomyController : BaseAdminController
    {
        private readonly ITermTaxonomyService _taxonomyService;

        public TaxonomyController(ITermTaxonomyService taxonomyService)
        {
            this._taxonomyService = taxonomyService;
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult GetCategoryList()
        {
            var categories = this._taxonomyService.GetPagedTaxonomy(TaxonomyType.Category);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateTermTaxonomyModel model)
        {
            var taxonomy = new TermTaxonomy();
            taxonomy.Count = 0;
            taxonomy.Description = model.Description;
            taxonomy.Taxonomy = model.Taxonomy;
            taxonomy.Term = new Term
            {
                Name = model.Name,
                Slug = model.Slug
            };

            this._taxonomyService.Create(taxonomy);

            return Json("true");
        }
        // GET: Taxonomy
        public ActionResult Index()
        {
            return View();
        }
    }
}