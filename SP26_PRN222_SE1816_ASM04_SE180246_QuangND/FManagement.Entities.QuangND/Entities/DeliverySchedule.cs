using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class DeliverySchedule
{
    public int ScheduleId { get; set; }

    public int OrderId { get; set; }

    public string? DriverName { get; set; }

    public string? VehiclePlate { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? EstimatedArrivalTime { get; set; }

    public DateTime? ActualArrivalTime { get; set; }

    public string? DeliveryStatus { get; set; }

    public string? StartLocation { get; set; }

    public string? EndLocation { get; set; }

    public double? TemperatureLog { get; set; }

    public string? ProofOfDelivery { get; set; }

    public virtual ICollection<DeliveryIssue> DeliveryIssues { get; set; } = new List<DeliveryIssue>();

    public virtual StoreOrder Order { get; set; } = null!;
}
