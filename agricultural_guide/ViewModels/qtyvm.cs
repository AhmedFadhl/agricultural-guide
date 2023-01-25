using agricultural_guide.Models;
namespace agricultural_guide.ViewModels
{
    public class qtyvm
    {
        public string crop_id { get; set; }
        public List<crop> crop { get; set; }
        public List<fertilizer> fertilizer { get; set; }
        public IEnumerable<IGrouping< string,IEnumerable<qty>>> fertilizer_Stage { get; set; }
        public List< qty> qty { get; set; }
        public List< fer_type> fer_Type { get; set; }
        public List<string> fer { get; set; }
    }
}
