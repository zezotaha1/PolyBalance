using PolyBalance.DTO;

namespace PolyBalance.Services.Stores
{
    public interface IStoresServices
    {
        public Task<StoreDTO> GetStoreByIdAsync(int id);
        public Task<ICollection<StoreDTO>> GetAllStoresAsync();
        public Task<StoreDTO> CreateStoreAsync(StoreDTO StoreDTO);
        public Task<StoreDTO> UpdateStoreAsync(StoreDTO StoreDTO);
        public Task DeleteStoreAsync(int id);
        public Task<StoreDTO> RestoreStoreAsync(int id);
    }
}
