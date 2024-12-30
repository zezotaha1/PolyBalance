using PolyBalance.DTO;

namespace PolyBalance.Services.Stores
{
    public interface IStoresServices
    {
        public Task<StoreDTO> GetStoreByIdAsync(int id);
        public Task<ICollection<StoreDTO>> GetAllStoresAsync();
        public Task CreateStoreAsync(StoreDTO StoreDTO);
        public Task UpdateStoreAsync(StoreDTO StoreDTO);
        public Task DeleteStoreAsync(int id);
        public Task RestoreStoreAsync(int id);
    }
}
