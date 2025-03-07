using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class AccountDetail : IActivatable
{
    public int AccountDetailId { get; set; }

    public int PartyId { get; set; } 
    public int AccountDetailType { get; set; }

    public int? OrderId { get; set; }

    public decimal AccountDetailAmount { get; set; }

    public string? AccountDetailNote { get; set; }

    public DateTime AccountDetailCreatedAt { get; set; }
    public bool IsActive { get; set; }

    public virtual Party Party { get; set; }
}
