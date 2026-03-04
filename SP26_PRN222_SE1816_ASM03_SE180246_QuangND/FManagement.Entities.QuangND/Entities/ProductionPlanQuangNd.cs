using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class ProductionPlanQuangNd
{
    public int PlanId { get; set; }

    public int StoreOrderItemId { get; set; }

    public int KitchenId { get; set; }

    public DateOnly PlanDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? PlanStatus { get; set; }

    public int? PriorityLevel { get; set; }

    public int? TotalExpectedQuantity { get; set; }

    public int? ActualProducedQuantity { get; set; }

    public string? BatchNotes { get; set; }

    public int? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual CentralKitchen Kitchen { get; set; } = null!;

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual StoreOrderItemQuangNd StoreOrderItem { get; set; } = null!;
}
