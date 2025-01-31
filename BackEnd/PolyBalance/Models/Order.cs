using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PolyBalance.Models;

public partial class Order : IActivatable
{
    public int OrderId { get; set; }

    public int PartyId { get; set; }

    public bool OrderType { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal OrderPaid { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Party? Party { get; set; }

    // Computed Properties
    public decimal OrderTotalAmount => OrderDetails?.Sum(od => od.TotalPrice) ?? 0;
    public decimal OrderLineTotal => OrderDetails?.Sum(od => od.LineTotal) ?? 0;
    public decimal OrderRemender => OrderTotalAmount - OrderPaid;
}
