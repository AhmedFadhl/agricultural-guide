using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Models
{
    public class qty
    {
        public string  _id { get; set; }
        public int QTY { get; set; }
        public string crop_id { get; set; }
        public string stages_fertilizer_id { get; set; }
        public string fertilizer_type_id { get; set; }
        public string fertilizer_id { get; set; }
        public virtual IEnumerable<SelectListItem> crops { get; set; }
        public virtual IEnumerable<SelectListItem> fer_types { get; set; }
        public virtual IEnumerable<SelectListItem> stages { get; set; }
        public virtual IEnumerable<SelectListItem> fertilizers { get; set; }
    }
}
