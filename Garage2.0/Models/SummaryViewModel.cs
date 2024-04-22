using Garage2._0.Data;

namespace Garage2._0.Models
{
    public class SummaryViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public List<int> Spots { get; set; }

        public SummaryViewModel(IEnumerable<Vehicle>  vehicles, List<int> spots)
        {
            Vehicles = vehicles;
            Spots = spots;
        }
        
    }
}
