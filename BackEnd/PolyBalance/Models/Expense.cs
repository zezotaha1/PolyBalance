using PolyBalance.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models;

public partial class Expense : IActivatable
{
    public int ExpenseId { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public double Amount { get; set; }
    [MaxLength(50)]
    public string? ExpenseType { get; set; }
    public string? Note{ get; set; }
    public bool IsActive { get; set; }

}
