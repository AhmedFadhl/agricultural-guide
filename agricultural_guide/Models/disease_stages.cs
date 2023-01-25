using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class disease_stages
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string crop_id { get; set; }
        public string disease_id { get; set; }
        public string description { get; set; }
        public virtual IEnumerable<SelectListItem> crops { get; set; }
        public virtual IEnumerable<SelectListItem> diseases { get; set; }


    }
}
