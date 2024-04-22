using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models;

public class Vehicle {

    
    public int VehicleId { get; init; }
    
    
    [Required]
    [StringLength(40, ErrorMessage = "String cannot exceed 40 characters.")]
    [RegularExpression("^[A-Z]{3}\\d{3}$", ErrorMessage = "License Plate must be in the format 'ABC123'.")]
    public string? LicensePlate { get; set; }

    [Required]
    public VehicleType VehicleType { get; set; }
    
    [Required]
    [StringLength(40, ErrorMessage = "String cannot exceed 40 characters.")]
    public string? Color { get; set; }

    [Required]
    [StringLength(40, ErrorMessage = "String cannot exceed 40 characters.")]
    public string? Brand { get; set; }

    [Required]
    [StringLength(40, ErrorMessage = "String cannot exceed 40 characters.")]
    public string? Model { get; set; }
    
    [Required]
    [Range(0, 10)]
    public int NumberOfWheels { get; set; }

    [Required]
    public DateTime ArrivalTime { get; set; }

    public DateTime? DepartureTime { get; set; }

    public ParkingSpot? ParkingSpot { get; set; } 
}