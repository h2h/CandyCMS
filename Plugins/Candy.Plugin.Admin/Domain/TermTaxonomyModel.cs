using System.ComponentModel.DataAnnotations;

namespace Candy.Plugin.Admin.Domain
{
    public class CreateTermTaxonomyModel
    {
        public int Parent { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Taxonomy { get; set; }
    }
}
