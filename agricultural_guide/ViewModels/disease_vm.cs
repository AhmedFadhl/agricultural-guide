using agricultural_guide.Models;
namespace agricultural_guide.ViewModels
{
    public class disease_vm
    {
        public List<disease> diseases { get; set; }
        public List<crop> crops { get; set; }
        public List<disease_stages> clutrication_Stages { get; set; }
        public IEnumerable<IGrouping<string,disease_stages>> clutrication_Stage { get; set; }
        public List<disease_type> disease_types { get; set; }
        public string crop_id { get; set; }

    }
}
