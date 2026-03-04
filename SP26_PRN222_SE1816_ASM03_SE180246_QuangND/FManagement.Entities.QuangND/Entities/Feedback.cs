using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int StoreId { get; set; }

    public int OrderId { get; set; }

    public int UserAccountId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual StoreOrder Order { get; set; } = null!;

    public virtual FranchiseStore Store { get; set; } = null!;
}
