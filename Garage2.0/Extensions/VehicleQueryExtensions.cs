using Garage2._0.Models;

namespace Garage2._0.Extensions;

public static class VehicleQueryExtensions {
    public static IQueryable<Vehicle> WhereActive(this IQueryable<Vehicle> vehicles) {
        return vehicles.Where(v => v.DepartureTime == null); 
    }
}