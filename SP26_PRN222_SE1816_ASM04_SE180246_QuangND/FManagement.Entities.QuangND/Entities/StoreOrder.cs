using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class StoreOrder
{
    public int OrderId { get; set; }

    public int StoreId { get; set; }

    public int KitchenId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDeliveryDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public string? PaymentStatus { get; set; }

    public string? ShippingAddress { get; set; }

    public string? ContactPhone { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<DeliverySchedule> DeliverySchedules { get; set; } = new List<DeliverySchedule>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual CentralKitchen Kitchen { get; set; } = null!;

    public virtual FranchiseStore Store { get; set; } = null!;

    public virtual ICollection<StoreOrderItemQuangNd> StoreOrderItemQuangNds { get; set; } = new List<StoreOrderItemQuangNd>();
}
