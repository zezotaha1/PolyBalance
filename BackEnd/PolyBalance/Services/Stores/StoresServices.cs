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
            return new StoreDTO
            {
                Id = store.StoreId,
                Name = store.StoreName,
                Address = store.StoreAddress,
                Capacity = store.StoreCapacity
            };
        }

        public async Task<ICollection<StoreDTO>> GetAllStoresAsync()
        {
            var storesFromDatabase = await _repositoryStore.GetAllAsync();
            var stores = new List<StoreDTO>();
            foreach (var store in storesFromDatabase)
            {
                stores.Add(new StoreDTO
                {
                    Id = store.StoreId,
                    Name = store.StoreName,
                    Address = store.StoreAddress,
                    Capacity = store.StoreCapacity
                });
            }
            return stores;
        }

        public async Task CreateStoreAsync(StoreDTO StoreDTO)
        {
            if(await _validation.NameValidationAsync(StoreDTO.Name))
            {
                var store = new Store { 
                    IsActive = true,
                    StoreName = StoreDTO.Name,
                    StoreCapacity = StoreDTO.Capacity,
                    StoreAddress = StoreDTO.Address
                };
                await _repositoryStore.AddAsync(store);
            }
        }

        public Task UpdateStoreAsync(StoreDTO StoreDTO)
        {
            throw new NotImplementedException();
        }
        
        public Task DeleteStoreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RestoreStoreAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
