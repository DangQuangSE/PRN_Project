using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public int ProductTypeId { get; set; }

    public int InventoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductCode { get; set; }

    public decimal? BasePrice { get; set; }

    public decimal? SellingPrice { get; set; }

    public string? UnitOfMeasure { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public double? Calories { get; set; }

    public int? PreparationTime { get; set; }

    public string? ItemCategory { get; set; }

    public int? ShelfLifeDays { get; set; }

    public bool? IsBatchTracked { get; set; }

    public decimal? MinOrderQty { get; set; }

    public int? LeadTimeDays { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<StoreOrderItemQuangNd> StoreOrderItemQuangNds { get; set; } = new List<StoreOrderItemQuangNd>();
}
