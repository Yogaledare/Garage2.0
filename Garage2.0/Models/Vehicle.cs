using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models;

public class Vehicle {

    public int VehicleId { get; init; }
    
    public string LicensePlate { get; set; } = string.Empty;

    public VehicleType? VehicleType { get; set; }

    public string Color { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int NumberOfWheels { get; set; }

    public DateTime ArrivalTime { get; set; }
}