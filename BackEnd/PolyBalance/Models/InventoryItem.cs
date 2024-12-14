using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class InventoryItem : IActivatable
{
    public int InventoryItemId { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool Type { get; set; } //0 means material, 1 means product

    [MaxLength(50)]
    public string? Unit { get; set; }

    public double? CurrentStock { get; set; } = 0;

    public double? ReorderLevel { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<ItemsPricesAndStore> ItemsPricesAndStores { get; set; } = new List<ItemsPricesAndStore>();

    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();
}
