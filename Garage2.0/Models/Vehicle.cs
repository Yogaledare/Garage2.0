using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models;

public class Vehicle {

    
    public int VehicleId { get; init; }
        
    
    [RegularExpression("^[A-Z]{3}\\d{3}$", ErrorMessage = "License Plate must be in the format 'ABC123'.")]
    public string LicensePlate { get; set; }

    public VehicleType VehicleType { get; set; }
    
    public string Color { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public int NumberOfWheels { get; set; }

    public DateTime ArrivalTime { get; set; }
}