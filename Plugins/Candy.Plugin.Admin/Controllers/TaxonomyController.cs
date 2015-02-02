using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Candy.Core.Domain;

namespace Candy.Plugin.Admin.Controllers
{
    public class TaxonomyController : BaseAdminController
    {
        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(FormCollection collection)
        {
            var taxonomy = new TermTaxonomy();
            taxonomy.Count = 0;
            taxonomy.Description = string.Empty;
            taxonomy.Taxonomy = "";

            var term = new Term();
            term.Name = "";
            term.Slug = "";

            return View();
        }
        // GET: Taxonomy
        public ActionResult Index()
        {
            return View();
        }
    }
}