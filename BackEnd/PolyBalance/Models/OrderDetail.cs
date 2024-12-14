using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class OrderDetail : IActivatable
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ItemsPricesAndStoreId { get; set; }

    public double Quantity { get; set; }

    public double UnitPrice { get; set; }
    public double TotalPrice {  get; set; }
    public float Discount { get; set; } = 0;// by Percent[0.00,1.00]
    public double LineTotal { get; set; }
    public bool IsActive { get; set; }

    public virtual ItemsPricesAndStore? ItemPrice { get; set; }

    public virtual Order? Order { get; set; }
}
