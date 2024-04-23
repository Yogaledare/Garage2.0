namespace Garage2._0.Models;

public class HomeViewModel {

    public List<VehicleTypeSummary> VehicleTypeSummaries { get; set; } = [];
    public List<AggregatedTimeEntry> TimeEntries { get; set; } = []; 
}

public class VehicleTypeSummary {
    public VehicleType VehicleType { get; set; }
    public int Count { get; set; }
    public int TotalWheels { get; set; }
}


public class AggregatedTimeEntry {
    public DateTime TimeStamp { get; set; }
    public int VehicleCount { get; set; }
}


public enum VehicleCountChange {
    Enter, Leave, 
}


    
// public VehicleType VehicleType { get; set; }
// public int Count { get; set; }
// public int TotalWheels { get; set; }



