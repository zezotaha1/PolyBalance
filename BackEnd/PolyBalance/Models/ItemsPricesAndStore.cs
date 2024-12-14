using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class ItemsPricesAndStore : IActivatable
{
    public int ItemsPricesAndStoreId { get; set; }

    public int? InventoryItemId { get; set; }

    public int? StoreId { get; set; }

    public double UnitCost { get; set; }

    public double? UnitSellingPrice { get; set; }

    public DateOnly StorageDate { get; set; }

    public double CurrentStock { get; set; }
    public bool IsActive { get; set; }


    public virtual ICollection<InventoryAdjustment> InventoryAdjustments { get; set; } = new List<InventoryAdjustment>();

    public virtual InventoryItem? Item { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; } = new List<ProductionMaterial>();

    public virtual Store? Store { get; set; }
}
