namespace agricultural_guide.Models
{
    public class Advertisers
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public string contact_link { get; set; }
        public IFormFile advertiser_image { get; set; }
        public string image_path { get; set; }


    }
}
