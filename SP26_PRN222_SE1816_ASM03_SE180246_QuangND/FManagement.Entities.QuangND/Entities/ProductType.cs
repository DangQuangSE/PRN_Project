using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
