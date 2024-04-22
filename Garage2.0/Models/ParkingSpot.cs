namespace Garage2._0.Models
{
    public class ParkingSpot
    {
        public int ParkingSpotId {  get; set; }
        public int Spot { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ParkingSpot(int spot, bool isAvailable) { 
            Spot = spot;
            IsAvailable = isAvailable;
        }

    }
}
