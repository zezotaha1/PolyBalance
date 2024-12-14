using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class AccountDetail : IActivatable
{
    public int AccountDetailId { get; set; }

    public int? PartyId { get; set; } 

    public bool? Type { get; set; } //0 mean debit , 1 mean credit

    public int? OprationId { get; set; }
    [MaxLength(50)]
    public string? OprationType {  get; set; }

    public double Amount { get; set; }

    public string? Note { get; set; }

    public bool IsActive { get; set; }

    public virtual Party? Party { get; set; }
}
