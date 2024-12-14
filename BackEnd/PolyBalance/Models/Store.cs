using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class Store : IActivatable
{
    public int StoreId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public double? Capacity { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<ItemsPricesAndStore> ItemsPricesAndStores { get; set; } = new List<ItemsPricesAndStore>();
}
