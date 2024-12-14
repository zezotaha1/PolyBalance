namespace PolyBalance.DTO
{
    public class PartyDTO
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Address { get; set; }

        public double Amount { get; set; }
    }
}
