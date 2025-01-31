namespace PolyBalance.DTO
{
    public class ItemPriceDTO
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public decimal UnitCost { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal CurrentStock { get; set; }
    }
}
