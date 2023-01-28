using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class Advertisments
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string id_advertiser { get; set; }
        public string image_path { get; set; }
        public IFormFile ads_image { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public virtual IEnumerable<SelectListItem> advertisers { get; set; }
    }
}
