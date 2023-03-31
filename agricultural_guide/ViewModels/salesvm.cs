using agricultural_guide.Models;

namespace agricultural_guide.ViewModels
{
    public class salesvm
    {
        public List<crop>  crops { get; set; }
        public List<Location> locations { get; set; }
        public List<sales_adviser> sales { get; set; }
        public string crop_id { get; set; }
        public IEnumerable<IGrouping<string,IEnumerable<sales_adviser>>> sales_by_loction { get; set; }
    }       
}
