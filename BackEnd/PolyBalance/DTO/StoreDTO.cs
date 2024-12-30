using System.ComponentModel.DataAnnotations;
namespace PolyBalance.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        public double Capacity { get; set; }
    }
}
