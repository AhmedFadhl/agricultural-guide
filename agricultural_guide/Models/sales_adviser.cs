using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class sales_adviser
    {

        public string crop_id { get; set; }
        public string loaction_id { get; set; }
        public double price { get; set; }
        public IEnumerable<SelectListItem> locations { get; set; }
        public IEnumerable<SelectListItem> crops { get; set; }
    }
}
