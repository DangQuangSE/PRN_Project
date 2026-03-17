using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class InventoryTransaction
{
    public int TransactionId { get; set; }

    public int InventoryId { get; set; }

    public string? TransactionType { get; set; }

    public double QuantityChange { get; set; }

    public double? PreviousQuantity { get; set; }

    public double? NewQuantity { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Reason { get; set; }

    public int? PerformedByAccountId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public int? OriginalTransactionId { get; set; }

    public bool IsReversal { get; set; }

    public string? ReversalReason { get; set; }

    public int? ReversedByAccountId { get; set; }

    public DateTime? ReversalDate { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;
}
