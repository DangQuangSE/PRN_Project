using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public int SupplierId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string? Sku { get; set; }

    public string? UnitOfMeasure { get; set; }

    public double? MinimumStockLevel { get; set; }

    public double? StorageTemperature { get; set; }

    public int? ShelfLifeDays { get; set; }

    public decimal? CurrentPrice { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual Supplier Supplier { get; set; } = null!;
}
