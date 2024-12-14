using PolyBalance.Repository;
using System.ComponentModel.DataAnnotations;

namespace PolyBalance.Models
{
    public class PartyType : IActivatable
    {
        public int PartyTypeId {  get; set; }
        [Required]
        [MaxLength(50)]
        public required string PartyTypeName { get; set; }
        public bool IsActive { get; set; }=true;

        public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
    }
}
