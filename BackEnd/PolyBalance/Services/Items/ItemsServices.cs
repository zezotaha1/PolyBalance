using PolyBalance.DTO;
using PolyBalance.Models;
using PolyBalance.Repository;
using PolyBalance.Services.ItemsPrices;
using PolyBalance.Services.parties;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PolyBalance.Services.Items
{
    public class ItemsServices : IItemsServices
    {
        private readonly IRepository<Item> _ItemRepository;
        private readonly Validation _validation;
        private readonly IItemsPricesServices _ItemsPricesServices;
        public ItemsServices(IRepository<Item> Repository, Validation validation, IItemsPricesServices itemsPricesServices) 
        {
            _ItemRepository = Repository;
            _validation = validation;
            _ItemsPricesServices = itemsPricesServices;
        }
        
        public async Task<ItemDTO> GetItemByIdAsync(int id)
        {
            return ToDTO(await _ItemRepository.GetByIdAsync(id));
        }
        
        public async Task<ICollection<ItemDTO>> GetAllItemsAsync()
        {
            var ItemsFromDatabase = await _ItemRepository.GetAllAsync();
            var Items = new List<ItemDTO>();
            foreach (var item in ItemsFromDatabase)
            {
                Items.Add(ToDTO(item));
            }
            return Items;
        }

        public async Task<ItemDTO> CreateItemAsync(ItemDTO ItemDTO)
        {
            ItemDTO.Id = 0;
            ItemDTO.CurrentStock = 0;
            _validation.NameValidationAsync(ItemDTO.Name);
            if (await _ItemRepository.IsUsedAsync(e=>e.ItemName== ItemDTO.Name))
            {
                throw new InvalidOperationException("This name has already been used.");
            }
            IsValidType(ItemDTO.Type);
            return ToDTO(await _ItemRepository.AddAsync(ToEntity(ItemDTO)));

        }

        public async Task<ItemDTO> UpdateItemAsync(ItemDTO ItemDTO)
        {
            var Item = await _ItemRepository.GetByIdAsync(ItemDTO.Id);
            if(Item.ItemName != ItemDTO.Name)
            {
                _validation.NameValidationAsync(ItemDTO.Name);
                if (await _ItemRepository.IsUsedAsync(e => e.ItemName == ItemDTO.Name))
                {
                    throw new InvalidOperationException("This name has already been used.");
                }
                Item.ItemName = ItemDTO.Name;
            }
            if(ItemDTO.CurrentStock != Item.ItemCurrentStock)
            {
                throw new Exception("You can not Change Current Stock From Here");
            }
            Item.ItemUnit = ItemDTO.Unit;
            Item.ItemDescription = ItemDTO.Description;
            Item.ItemReorderLevel = ItemDTO.ReorderLevel;
            IsValidType(ItemDTO.Type);
            Item.ItemType = ((ItemDTO.Type == "product") ? true : false);
            return ToDTO(await _ItemRepository.UpdateAsync(Item));

        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            if(item.CurrentStock>0)
            {
                throw new Exception("Thir are Stock For this Item ");
            }

            await _ItemRepository.DeleteByIdAsync(id);
        }

        public async Task<ItemDTO> RestoreItemAsync(int id)
        {
            return ToDTO(await _ItemRepository.RestoreAsync(e => e.ItemId == id));
        }

        private static bool IsValidType(string Type)
        {
            return (Type == "product" || Type == "material")?true:throw new Exception("This Type not Valid");
        }

        private ItemDTO ToDTO(Item Item)
        {
            var prices = _ItemsPricesServices.GetAllItemPricesForOneItemAsync(Item.ItemId).Result;
            return new ItemDTO
            {
                Id = Item.ItemId,
                Name = Item.ItemName,
                Type = (Item.ItemType ? "product" : "material"),
                CurrentStock = Item.ItemCurrentStock,
                ReorderLevel = Item.ItemReorderLevel,
                Description = Item.ItemDescription ?? "",
                ItemPrices = prices,
                Unit = Item.ItemUnit
            };
        }

        private static Item ToEntity(ItemDTO dto)
        {
            return new Item
            {
                ItemId = dto.Id,
                ItemName = dto.Name,
                ItemType = ((dto.Type== "product")?true:false),
                ItemCurrentStock = dto.CurrentStock,
                ItemReorderLevel = dto.ReorderLevel,
                ItemDescription = dto.Description ?? "",
                ItemUnit = dto.Unit,
                IsActive = true
            };
        }
    }
}
