namespace agricultural_guide.Models
{
    public class users
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string uid { get; set; }
        public string pass { get; set; }
        public string about { get; set; }
        public string specialize { get; set; }
        public string image_path { get; set; }
        public IFormFile user_image { get; set; }
        public int state { get; set; }
        public int usertype { get; set; }
        public int created_at { get; set; }

    }
}
