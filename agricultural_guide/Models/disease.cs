using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class disease
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string[] symptoms { get; set; }
        public string reason { get; set; }
        public string[] prevention { get; set; }
        public string recommendations { get; set; }
        public string antiorganic { get; set; }
        public string chemicalcontrol { get; set; }
        public string dis_type_id { get; set; }
        public string image_path { get; set; }
        public IFormFile disease_image { get; set; }
        public virtual IEnumerable<SelectListItem> disease_types { get; set; }

    }
}
