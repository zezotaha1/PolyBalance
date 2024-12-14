using PolyBalance.Repository;
using System;
using System.Collections.Generic;

namespace PolyBalance.Models;

public partial class ProductionMaterial : IActivatable
{
    public int ProductionMaterialId { get; set; }

    public int? ProductionOrderId { get; set; }

    public int? ItemsPricesAndStoreId { get; set; }

    public double QuantityUsedPerUnit { get; set; }
    public bool IsActive { get; set; }
    public virtual ItemsPricesAndStore? ItemPrice { get; set; }

    public virtual ProductionOrder? ProductionOrder { get; set; }
}
