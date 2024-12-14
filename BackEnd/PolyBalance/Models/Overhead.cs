using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Overhead : IActivatable
{
    public int OverheadId { get; set; }

    public int? ProductionOrderId { get; set; }

    [MaxLength(50)]
    public required string OverheadType { get; set; }

    public double? Cost { get; set; }

    public bool IsActive { get; set; }

    public virtual ProductionOrder? ProductionOrder { get; set; }
}
