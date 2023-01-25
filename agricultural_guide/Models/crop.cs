using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;


namespace agricultural_guide.Models
{
    public class crop
    {

        public string _id { get; set; }
        [Key]
        public int crop_id { get; set; }
        public string name { get; set; }
        public string image_path { get; set; }
        public IFormFile crop_image { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public int state { get; set; }
        public string crop_type { get; set; }
        public virtual IEnumerable<SelectListItem> crop_types { get; set; }
        public double created_at { get; set; }




    }
    
}
