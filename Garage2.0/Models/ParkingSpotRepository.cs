using Garage2._0.Data;

namespace Garage2._0.Models
{
    public class ParkingSpotRepository
    {
        private const int TOTAL_PARKING_SPOT = 100;
        private int currentUsed = 0;
        private HashSet<int> usedSpots = new HashSet<int>();
        private Dictionary<int, Vehicle> parkedVehicles = new Dictionary<int, Vehicle>();
        private Random rand = new Random();
        private readonly Garage2_0Context _context;
        public ParkingSpotRepository(Garage2_0Context context)
        {
            _context = context;
            onInit();
        }
        public void onInit()
        {
            //Read all vehicles to repository
            foreach(var v in _context.Vehicles)
            {
                if(v.ParkingSpot == null)
                {
                    onParkVehicle(v);
                }
                else
                {
                    if (!usedSpots.Contains(v.ParkingSpot.Spot))
                    {
                        usedSpots.Add(v.ParkingSpot.Spot);
                        parkedVehicles.Add(v.ParkingSpot.Spot, v);
                        currentUsed++;
                    }
                }
            }
        }

        public Vehicle? onParkVehicle(Vehicle v)
        {
            if(currentUsed >= TOTAL_PARKING_SPOT)
            {
                return null;
            }
            else
            {
                //give a random spot              
                int s = rand.Next(1,TOTAL_PARKING_SPOT+1);
                while (usedSpots.Contains(s))
                {
                    s = rand.Next(1, TOTAL_PARKING_SPOT+1);
                }
                usedSpots.Add(s);
                v.ParkingSpot = new ParkingSpot(s, false);
                parkedVehicles.Add(s, v);
                currentUsed++;
                return v;
            }
        }

        public void onLeaveVehicle(Vehicle v)
        {

        }

        public List<Vehicle> AllParkedVehicles()
        {
            return parkedVehicles.Values.ToList();
        }
    }
}
