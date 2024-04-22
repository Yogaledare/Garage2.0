namespace Garage2._0.Models
{
    public class SpotViewModel
    {
        public List<int> Spots { get; set; }
        public SpotViewModel(List<int> spots)
        {
            Spots = spots;
        }
    }
}
