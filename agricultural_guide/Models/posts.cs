namespace agricultural_guide.Models
{
    public class posts
    {
        public string _id { get; set; }
        public string post { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public IFormFile post_image { get; set; }

    }
}
