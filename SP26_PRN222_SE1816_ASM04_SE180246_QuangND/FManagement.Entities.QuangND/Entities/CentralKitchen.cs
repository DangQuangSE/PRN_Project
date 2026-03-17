using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class CentralKitchen
{
    public int KitchenId { get; set; }

    public string KitchenName { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContactPerson { get; set; }

    public string? PhoneNumber { get; set; }

    public double? Capacity { get; set; }

    public string? OperatingStatus { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<ProductionPlanQuangNd> ProductionPlanQuangNds { get; set; } = new List<ProductionPlanQuangNd>();

    public virtual ICollection<StoreOrder> StoreOrders { get; set; } = new List<StoreOrder>();
}
