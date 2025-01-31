using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        [Required]
        public int PartyId { get; set; }

        public string Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal Paid { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal LineTotal { get; set; }
        public decimal Remender { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }
}
