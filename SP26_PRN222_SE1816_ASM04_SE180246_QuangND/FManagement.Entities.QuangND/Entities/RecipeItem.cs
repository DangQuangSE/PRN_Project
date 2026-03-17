using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class RecipeItem
{
    public int RecipeItemId { get; set; }

    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public decimal? QuantityRequired { get; set; }

    public string? UnitOfMeasure { get; set; }

    public decimal? WastePercentage { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
