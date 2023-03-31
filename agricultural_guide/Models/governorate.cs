using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class governorate
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string city_id { get; set; }
        public string description { get; set; }
        public IEnumerable<SelectListItem> cities { get; set; }
    }
}
