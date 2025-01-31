using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class InventoryAdjustment : IActivatable
{
    public int InventoryAdjustmentId { get; set; }

    public int? ItemsPricesId { get; set; }
    public bool AdjustmentType { get; set; } //0 mean wasteg ,1 mean add
    public double QuantityChange { get; set; }
    public DateTime AdjustmentDate { get; set; }
    public string? Reason { get; set; }
    public bool IsActive { get; set; }

    public virtual ItemPrice? ItemPrice { get; set; }
}
