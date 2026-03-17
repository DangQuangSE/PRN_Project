using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class ProductBatch
{
    public int BatchId { get; set; }

    public int PlanId { get; set; }

    public int ProductId { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Qcstatus { get; set; }

    public double? QuantityProduced { get; set; }

    public virtual ProductionPlanQuangNd Plan { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
