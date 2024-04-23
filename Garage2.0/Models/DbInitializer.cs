using Garage2._0.Data;
using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        var context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<Garage2_0Context>();

        if (context.Vehicles.Any())
        {
            return; // DB has been seeded
        }

        var random = new Random();
        var types = Enum.GetValues(typeof(VehicleType));
        var colors = new[] { "Red", "Blue", "Yellow", "White", "Black", "Green", "Grey" };
        var brands = new[] { "Toyota", "Ford", "Volvo", "Bayliner", "Honda", "Chevrolet", "Scania", "Yamaha", "Ram" };
        var models = new[] { "Corolla", "F-150", "7700", "VR5", "Civic", "Silverado", "Citywide", "242X", "1500", "Mustang" };

        var vehicles = new List<Vehicle>();

        // Generate 50 vehicles with random data
        for (int i = 0; i < 50; i++) {
            var arrivalTime = DateTime.Now.AddDays(-random.Next(0, 30)).AddHours(-random.Next(0, 24));
            DateTime? departureTime;
            ParkingSpot? parkingSpot = null;


            // Increase the chance of having a departure time
            if (random.Next(100) < 75) // 75% chance to have a departure time
            {
                departureTime = arrivalTime.AddHours(random.Next(1, 48));

                if (departureTime >= DateTime.Now) {
                    departureTime = null;
                    parkingSpot = new ParkingSpot(i + 1, false);
                }
            }

            else {
                parkingSpot = new ParkingSpot(i + 1, false);
                departureTime = null;
            }

            vehicles.Add(new Vehicle {
                LicensePlate =
                    $"{(char) ('A' + random.Next(0, 26))}{(char) ('A' + random.Next(0, 26))}{(char) ('A' + random.Next(0, 26))}{random.Next(100, 999)}",
                VehicleType = (VehicleType) types.GetValue(random.Next(types.Length)),
                Color = colors[random.Next(colors.Length)],
                Brand = brands[random.Next(brands.Length)],
                Model = models[random.Next(models.Length)],
                NumberOfWheels = random.Next(0, 2) == 0 ? 4 : 6,
                ArrivalTime = arrivalTime,
                DepartureTime = departureTime,
                ParkingSpot = parkingSpot,
            });
        }

        context.Vehicles.AddRange(vehicles);
        context.SaveChanges();
    }
}
