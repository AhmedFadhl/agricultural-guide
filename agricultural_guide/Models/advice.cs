namespace agricultural_guide.Models
{
    public class advice
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public IFormFile advice_image { get; set; }
        public string[] How { get; set; }
        public string[] important { get; set; }
        public string created_at { get; set; }


    }
}
