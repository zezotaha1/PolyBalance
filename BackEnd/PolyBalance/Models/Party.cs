using Microsoft.EntityFrameworkCore;
using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;
public partial class Party : IActivatable
{
    public int PartyId { get; set; }

    public int PartyTypeId { get; set; }

    [MaxLength(50)]
    [Required]
    public required string PartyName { get; set; }
  
    [MaxLength(11)]
    [Required]
    public required string PartyPhoneNumber { get; set; }
    [MaxLength(255)]
    public string? PartyAddress { get; set; }

    public int PartyRateing { get; set; } = 0;
    public double PartyTotalAmount { get; set; }//sum all acount details
    public bool IsActive { get; set; }

    public virtual PartyType? PartyType { get; set; }
    public virtual ICollection<AccountDetail> AccountDetails { get; set; } = new List<AccountDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
