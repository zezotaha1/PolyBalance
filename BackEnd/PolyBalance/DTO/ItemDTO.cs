using System.ComponentModel.DataAnnotations;

namespace PolyBalance.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required string Type { get; set; } //0 means material, 1 means product

        [MaxLength(20)]
        public required string Unit { get; set; }

        public double CurrentStock { get; set; }

        public double ReorderLevel { get; set; }

        public ICollection<ItemPriceDTO>? ItemPrices { get; set; }
    }
}
