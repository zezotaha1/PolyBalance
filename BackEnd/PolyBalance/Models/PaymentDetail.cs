using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class PaymentDetail : IActivatable
{
    public int PaymentDetailId { get; set; }

    public int? TransactionId { get; set; }
    [MaxLength(50)]
    public required string PaymentMethod { get; set; }
    public string? ImagePath { get; set; }
    public double Amount { get; set; }
    public bool IsActive { get; set; }
    public virtual Transaction? Transaction { get; set; }
}
