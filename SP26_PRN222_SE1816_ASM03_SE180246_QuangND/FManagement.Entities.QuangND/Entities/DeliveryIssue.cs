using System;
using System.Collections.Generic;

namespace FManagement.Entities.QuangND.Entities;

public partial class DeliveryIssue
{
    public int IssueId { get; set; }

    public int ScheduleId { get; set; }

    public string? IssueType { get; set; }

    public string? Description { get; set; }

    public DateTime? ReportedTime { get; set; }

    public string? Resolution { get; set; }

    public virtual DeliverySchedule Schedule { get; set; } = null!;
}
