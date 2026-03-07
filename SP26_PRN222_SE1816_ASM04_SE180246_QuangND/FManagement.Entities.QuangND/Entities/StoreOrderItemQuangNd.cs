using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class StoreOrderItemQuangNd
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int QuantityOrdered { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual StoreOrder Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductionPlanQuangNd> ProductionPlanQuangNds { get; set; } = new List<ProductionPlanQuangNd>();
}
