using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class InventoryType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsPerishable { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
