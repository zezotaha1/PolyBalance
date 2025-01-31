using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class ItemPrice : IActivatable
{
    public int ItemPriceId { get; set; }

    public int ItemId { get; set; }

    public decimal ItemPriceUnitCost { get; set; }

    public decimal ItemPriceCurrentStock { get; set; }

    public DateTime ItemPriceCreatedAt { get; set; }

    public bool IsActive { get; set; }


    public virtual ICollection<InventoryAdjustment> InventoryAdjustments { get; set; } = new List<InventoryAdjustment>();

    public virtual Item? Item { get; set; }
    public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; } = new List<ProductionMaterial>();
    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
}
