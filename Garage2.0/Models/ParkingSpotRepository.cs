using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;

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
            init();
        }
      
         public void init()
        {
            var vehicles = _context.Vehicles.Include(v => v.ParkingSpot);
            foreach (var vehicle in vehicles)
            {
                //TO, check null
                if (vehicle.ParkingSpot != null)
                {
                    if (!parkedVehicles.ContainsKey(vehicle.ParkingSpot.Spot))
                    {
                        parkedVehicles.Add(vehicle.ParkingSpot.Spot, vehicle);
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
            if (v.ParkingSpot!= null && parkedVehicles.Keys.Contains(v.ParkingSpot.Spot))
            {
                parkedVehicles.Remove(v.ParkingSpot.Spot);
                usedSpots.Remove(v.ParkingSpot.Spot);
                currentUsed--;
                v.ParkingSpot = null;
            }
        }

        public List<int> AllParkedVehiclesIndex()
        {
            return parkedVehicles.Keys.ToList();
        }
    }
}
