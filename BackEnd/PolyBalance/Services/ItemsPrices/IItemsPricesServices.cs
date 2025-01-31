using PolyBalance.DTO;

namespace PolyBalance.Services.ItemsPrices
{
    public interface IItemsPricesServices
    {
        public Task<ItemPriceDTO> GetItemPriceByIdAsync(int id);
        public Task<ICollection<ItemPriceDTO>> GetAllItemPricesAsync();
        public Task<ICollection<ItemPriceDTO>> GetAllItemPricesForOneItemAsync(int id);
        public Task<ItemPriceDTO> CreateItemPriceAsync(ItemPriceDTO ItemPriceDTO);
        public Task<ItemPriceDTO> UpdateItemPriceAsync(ItemPriceDTO ItemPriceDTO);
        public Task DeleteItemPriceAsync(int id);
        public Task<ItemPriceDTO> RestoreItemPriceAsync(int id);
    }
}
