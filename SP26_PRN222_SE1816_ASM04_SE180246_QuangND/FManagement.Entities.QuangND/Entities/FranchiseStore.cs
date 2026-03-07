using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class FranchiseStore
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Address { get; set; }

    public string? OwnerName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateOnly? OpeningDate { get; set; }

    public string? ContractStatus { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<StoreOrder> StoreOrders { get; set; } = new List<StoreOrder>();
}
