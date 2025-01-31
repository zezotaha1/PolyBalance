using System.ComponentModel.DataAnnotations;

namespace PolyBalance.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UintId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be positive")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1")]
        public decimal Discount { get; set; } = 0; // Percentage

        public decimal LineTotal => TotalPrice * (1 - Discount);
    }
}
