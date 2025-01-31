using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;
using System.Data.SqlTypes;
using System.Data;
using PolyBalance.Repository;

namespace PolyBalance.Services.Stores
{
    public class StoresServices : IStoresServices
    {
        private readonly Validation _validation;
        private readonly IRepository<Store> _repositoryStore;

        public StoresServices(IRepository<Store> Repository, Validation validation)
        {

            _repositoryStore = Repository;
            _validation = validation;
        }

        public async Task<StoreDTO> GetStoreByIdAsync(int id)
        {
            var store = await _repositoryStore.GetByIdAsync(id);
            return ToDTO(store);
        }

        public async Task<ICollection<StoreDTO>> GetAllStoresAsync()
        {
            var storesFromDatabase = await _repositoryStore.GetAllAsync();
            var stores = new List<StoreDTO>();
            foreach (var store in storesFromDatabase)
            {
                stores.Add(ToDTO(store));
            }
            return stores;
        }

        public async Task<StoreDTO> CreateStoreAsync(StoreDTO StoreDTO)
        {
            StoreDTO.Id = 0;
            _validation.NameValidationAsync(StoreDTO.Name);
            
            if(await _repositoryStore.IsUsedAsync(s=>s.StoreName == StoreDTO.Name&&s.StoreAddress == StoreDTO.Address))
            {
                throw new Exception("this name and address together have already been used.");
            }
            return ToDTO(await _repositoryStore.AddAsync(ToEntity(StoreDTO)));
        }

        public async Task<StoreDTO> UpdateStoreAsync(StoreDTO StoreDTO)
        {
            var store = await _repositoryStore.GetByIdAsync(StoreDTO.Id);
            _validation.NameValidationAsync(StoreDTO.Name);

            if (await _repositoryStore.IsUsedAsync(s => s.StoreName == StoreDTO.Name && s.StoreAddress == StoreDTO.Address))
            {
                throw new Exception("this name and address together have already been used.");
            }
            return ToDTO(await _repositoryStore.UpdateAsync(ToEntity(StoreDTO)));
        }
        
        public async Task DeleteStoreAsync(int id)
        {
            await _repositoryStore.DeleteByIdAsync(id);
        }

        public async Task<StoreDTO> RestoreStoreAsync(int id)
        {
            return ToDTO(await _repositoryStore.RestoreAsync(entity => entity.StoreId == id));
        }

        private static StoreDTO ToDTO(Store store)
        {
            return new StoreDTO
            {
                Id =store.StoreId,
                Name = store.StoreName,
                Address = store.StoreAddress,
                Capacity = store.StoreCapacity
            };
        }

        private static Store ToEntity(StoreDTO dto)
        {
            return new Store
            {
                StoreId = dto.Id,
                StoreName = dto.Name,
                StoreAddress = dto.Address,
                StoreCapacity = dto.Capacity,
                IsActive = true
            };
        }

    }
}
