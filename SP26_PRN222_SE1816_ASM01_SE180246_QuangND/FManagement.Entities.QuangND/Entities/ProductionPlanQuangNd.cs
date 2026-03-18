using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FManagement.Entities.QuangND.Entities;

public partial class ProductionPlanQuangNd
{
    public int PlanId { get; set; }

    [Required(ErrorMessage = "StoreOrderItemId is required!")]
    public int StoreOrderItemId { get; set; }

    [Required(ErrorMessage = "KitchenId is required!")]
    public int KitchenId { get; set; }

    public DateOnly PlanDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? PlanStatus { get; set; }

    public int? PriorityLevel { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "TotalExpectedQuantity must be a non-negative integer!")]
    public int? TotalExpectedQuantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Produced Quantity must be a non-negative integer!")]
    public int? ActualProducedQuantity { get; set; }

    [StringLength(10, ErrorMessage = "BatchNote must have at least 10 characters!")]
    public string? BatchNotes { get; set; }

    public int? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual CentralKitchen Kitchen { get; set; } = null!;

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual StoreOrderItemQuangNd StoreOrderItem { get; set; } = null!;
}
