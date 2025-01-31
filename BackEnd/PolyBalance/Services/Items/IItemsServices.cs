using PolyBalance.DTO;

namespace PolyBalance.Services.Items
{
    public interface IItemsServices
    {
        public Task<ItemDTO> GetItemByIdAsync(int id);
        public Task<ICollection<ItemDTO>> GetAllItemsAsync();
        public Task<ItemDTO> CreateItemAsync(ItemDTO ItemDTO);
        public Task<ItemDTO> UpdateItemAsync(ItemDTO ItemDTO);
        public Task DeleteItemAsync(int id);
        public Task<ItemDTO> RestoreItemAsync(int id);
    }
}
