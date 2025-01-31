using PolyBalance.Repository;
using System;

namespace PolyBalance.Models;

public partial class OrderDetail : IActivatable
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ItemPriceId { get; set; }

    public decimal OrderDetailQuantity { get; set; }

    public decimal OrderDetailUnitPrice { get; set; }

    public decimal TotalPrice => OrderDetailQuantity * OrderDetailUnitPrice;

    public decimal OrderDetailDiscount { get; set; } = 0; // [0.00,1.00] Percentage

    public decimal LineTotal => TotalPrice * (1 - OrderDetailDiscount); // Discounted total

    public bool IsActive { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ItemPrice? ItemPrice { get; set; }
}
