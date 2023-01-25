using Microsoft.AspNetCore.Mvc.Rendering;
using agricultural_guide.Models;

namespace agricultural_guide.Models
{
    public class fer_type
    {
        public string _id { get; set; }
        public string name { get; set; }
        public List<string> fertilizer { get; set; }
        public IEnumerable<SelectListItem> fertilizers { get; set; }
    }
}
