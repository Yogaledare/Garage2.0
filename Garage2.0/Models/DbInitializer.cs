using Garage2._0.Data;

namespace Garage2._0.Models;

public static class DbInitializer {
    public static void Seed(IApplicationBuilder applicationBuilder) {
        var context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<Garage2_0Context>();

        if (context.Vehicles.Any()) {
            return;
        }

        List<Vehicle> vehicleSeed = [
            new Vehicle {
                LicensePlate = "ABC123",
                VehicleType = VehicleType.Car,
                Color = "Red",
                Brand = "Toyota",
                Model = "Corolla",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now,      
                ParkingSpot = new ParkingSpot(1,false)
            },
            new Vehicle {
                LicensePlate = "DEF456",
                VehicleType = VehicleType.Truck,
                Color = "Blue",
                Brand = "Ford",
                Model = "F-150",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now.AddHours(-1),
                 ParkingSpot = new ParkingSpot(2,false)
            },
            new Vehicle {
                LicensePlate = "GHI789",
                VehicleType = VehicleType.Bus,
                Color = "Yellow",
                Brand = "Volvo",
                Model = "7700",
                NumberOfWheels = 6,
                ArrivalTime = DateTime.Now.AddHours(-2),
                 ParkingSpot = new ParkingSpot(3,false)
            },
            new Vehicle {
                LicensePlate = "JKL012",
                VehicleType = VehicleType.Boat,
                Color = "White",
                Brand = "Bayliner",
                Model = "VR5",
                NumberOfWheels = 0,
                ArrivalTime = DateTime.Now.AddHours(-3),
                 ParkingSpot = new ParkingSpot(4,false)
            },
            new Vehicle {
                LicensePlate = "MNO345",
                VehicleType = VehicleType.Car,
                Color = "Black",
                Brand = "Honda",
                Model = "Civic",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now.AddHours(-4),
                 ParkingSpot = new ParkingSpot(5,false)
            },
            new Vehicle {
                LicensePlate = "PQR678",
                VehicleType = VehicleType.Truck,
                Color = "Green",
                Brand = "Chevrolet",
                Model = "Silverado",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now.AddHours(-5),
                 ParkingSpot = new ParkingSpot(6,false)
            },
            new Vehicle {
                LicensePlate = "STU901",
                VehicleType = VehicleType.Bus,
                Color = "Red",
                Brand = "Scania",
                Model = "Citywide",
                NumberOfWheels = 6,
                ArrivalTime = DateTime.Now.AddHours(-6),
                 ParkingSpot = new ParkingSpot(7,false)
            },
            new Vehicle {
                LicensePlate = "VWX234",
                VehicleType = VehicleType.Boat,
                Color = "Blue",
                Brand = "Yamaha",
                Model = "242X",
                NumberOfWheels = 0,
                ArrivalTime = DateTime.Now.AddHours(-7),
                 ParkingSpot = new ParkingSpot(8,false)
            },
            new Vehicle {
                LicensePlate = "YZA567",
                VehicleType = VehicleType.Car,
                Color = "Grey",
                Brand = "Ford",
                Model = "Mustang",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now.AddHours(-8),
                 ParkingSpot = new ParkingSpot(9,false)
            },
            new Vehicle {
                LicensePlate = "BCD890",
                VehicleType = VehicleType.Truck,
                Color = "White",
                Brand = "Ram",
                Model = "1500",
                NumberOfWheels = 4,
                ArrivalTime = DateTime.Now.AddHours(-9),
                 ParkingSpot = new ParkingSpot(10,false)
            }
        ];

        context.AddRange(vehicleSeed);

        context.SaveChanges();
    }
}