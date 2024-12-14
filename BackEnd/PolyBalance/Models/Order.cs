using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Order : IActivatable
{
    public int OrderId { get; set; }

    public bool OrderType { get; set; } //0 means purchase, 1 means sale

    public int? PartyId { get; set; }

    public DateOnly OrderDate { get; set; }
    [MaxLength(50)]
    public string? Status { get; set; }

    public double TotalAmount { get; set; } //Total amount of the order :sum of all order details
    public float Discount { get; set; } = 0;// by Percent[0.00,1.00]
    public double LineTotal {  get; set; }

    public bool IsActive { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Party? Party { get; set; }
}
