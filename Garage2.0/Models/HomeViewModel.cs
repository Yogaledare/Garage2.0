namespace Garage2._0.Models;

public class HomeViewModel {

    public List<VehicleTypeSummary> VehicleTypeSummaries { get; set; } = [];
    public List<TimeEntry> TimeEntries { get; set; } = []; 
}

public class VehicleTypeSummary {
    public VehicleType VehicleType { get; set; }
    public int Count { get; set; }
    public int TotalWheels { get; set; }
}


public class TimeEntry {
    public DateTime TimeStamp { get; set; }
    public VehicleCountChange VehicleCountChange { get; set; }
}


public enum VehicleCountChange {
    Enter, Leave, 
}


    
// public VehicleType VehicleType { get; set; }
// public int Count { get; set; }
// public int TotalWheels { get; set; }



