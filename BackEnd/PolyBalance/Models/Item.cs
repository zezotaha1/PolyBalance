using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Item : IActivatable
{
    public int ItemId { get; set; }   
    [Required]
    [MaxLength(50)]
    public required string ItemName { get; set; }

    public string? ItemDescription { get; set; }

    public bool ItemType { get; set; } //0 means material, 1 means product

    [Required]
    [MaxLength(20)]
    public required string ItemUnit { get; set; }

    public required double ItemCurrentStock { get; set; } = 0;

    public DateTime ItemCreatedAt { get; set; } = new DateTime();

    public double ItemReorderLevel { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<ItemPrice>? ItemsPrices { get; set; }

    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();
}
