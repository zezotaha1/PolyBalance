using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Transaction : IActivatable
{
    public int TransactionId { get; set; }

    public bool TransactionType { get; set; } //0 means income, 1 means expense
    public int? OprationId { get; set; }
    [MaxLength(50)]
    public string? OprationType { get; set; }

    public DateTime TransactionDate { get; set; }

    public double Amount { get; set; }

    public string? Note { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
}
