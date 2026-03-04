using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? KitchenId { get; set; }

    public int? StoreId { get; set; }

    public int IngredientId { get; set; }

    public int TypeId { get; set; }

    public double? CurrentQuantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? WarehouseLocation { get; set; }

    public string? BatchNumber { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public double ReservedQuantity { get; set; }

    public bool IsBlocked { get; set; }

    public string? LastReversalReference { get; set; }

    public int ReversalCount { get; set; }

    public string? Notes { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    public virtual CentralKitchen? Kitchen { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual FranchiseStore? Store { get; set; }

    public virtual InventoryType Type { get; set; } = null!;
}
