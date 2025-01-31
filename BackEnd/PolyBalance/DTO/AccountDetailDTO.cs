using PolyBalance.Models;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.DTO
{
    public class AccountDetailDTO
    {
        public int Id { get; set; }

        public int PartyId { get; set; }

        public int Type { get; set; } 

        public int? OrderId { get; set; }

        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
    }
}
