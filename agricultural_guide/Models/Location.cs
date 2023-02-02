namespace agricultural_guide.Models
{
    public class Location
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string map_link { get; set; }
        public string image_path { get; set; }
        public IFormFile saleLocation_image { get; set; }

    }
}
