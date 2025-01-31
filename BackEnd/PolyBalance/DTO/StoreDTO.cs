using System.ComponentModel.DataAnnotations;
namespace PolyBalance.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Address { get; set; }

        public double Capacity { get; set; }
    }
}
