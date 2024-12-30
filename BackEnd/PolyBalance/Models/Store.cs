using PolyBalance.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Store : IActivatable
{
    public int StoreId { get; set; }

    [Required]
    [MaxLength(30)]
    public string StoreName { get; set; }

    [MaxLength(50)]
    public string? StoreAddress { get; set; }

    public double StoreCapacity { get; set; } = int.MaxValue; // Default to 0 if nullable

    public bool IsActive { get; set; }

    public virtual ICollection<ItemsPricesAndStore> ItemsPricesAndStores { get; set; } = new List<ItemsPricesAndStore>();
}
