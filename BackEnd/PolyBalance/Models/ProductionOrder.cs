using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class ProductionOrder : IActivatable
{
    public int ProductionOrderId { get; set; }

    public int? InventoryItemId { get; set; }

    public double ProductionQuantity { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public double UintCost { get; set; }//sum all materials cost and coverheads 
    [MaxLength(50)]
    public string? Status { get; set; }
    public bool IsActive { get; set; }
    public virtual InventoryItem? Item { get; set; }

    public virtual ICollection<Overhead> Overheads { get; set; } = new List<Overhead>();

    public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; } = new List<ProductionMaterial>();
}
