using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int ProductId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string? Version { get; set; }

    public decimal? StandardYield { get; set; }

    public string? YieldUnit { get; set; }

    public string? InstructionSteps { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public bool? IsDefault { get; set; }

    public string? Status { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
}
