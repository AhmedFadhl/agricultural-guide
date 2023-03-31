using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class Location
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string crop_id { get; set; }
        public string governorate_id { get; set; }
        public string location { get; set; }
        public string locationX { get; set; }
        public string locationY { get; set; }
        public string image_path { get; set; }
        public IFormFile saleLocation_image { get; set; }
        public IEnumerable<SelectListItem> crops { get; set; }
        public IEnumerable<SelectListItem> governorates { get; set; }

    }
}
