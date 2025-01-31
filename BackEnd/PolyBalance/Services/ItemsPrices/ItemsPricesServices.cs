using PolyBalance.DTO;
using PolyBalance.Models;

namespace PolyBalance.Services.ItemsPrices
{
    public class ItemsPricesServices : IItemsPricesServices
    {

        private readonly IRepository<ItemPrice> _ItemsPricesRepository;


        public ItemsPricesServices(IRepository<ItemPrice> repository)
        {
            _ItemsPricesRepository = repository;

        }

        public async Task<ItemPriceDTO> GetItemPriceByIdAsync(int id)
        {
            return ToDTO(await _ItemsPricesRepository.GetByIdAsync(id));
        }

        public async Task<ICollection<ItemPriceDTO>> GetAllItemPricesAsync()
        {
            var ItemPricesFromDatabase = await _ItemsPricesRepository.GetAllAsync();
            var ItemPrices = new List<ItemPriceDTO>();
            foreach (var ip in ItemPricesFromDatabase)
            {
                ItemPrices.Add(ToDTO(ip));
            }
            return ItemPrices;
        }

        public async Task<ICollection<ItemPriceDTO>> GetAllItemPricesForOneItemAsync(int id)
        {
            var ItemPricesFromDatabase = await _ItemsPricesRepository.FindAsync(e => e.ItemId == id );
            var ItemPrices = new List<ItemPriceDTO>();
            foreach (var ip in ItemPricesFromDatabase)
            {
                ItemPrices.Add(ToDTO(ip));
            }
            return ItemPrices;
        }

        public async Task<ItemPriceDTO> CreateItemPriceAsync(ItemPriceDTO ItemPriceDTO)
        {
            ItemPriceDTO.Id = 0;
            ItemPriceDTO.CurrentStock = 0;
            await _ItemsPricesRepository.IsIdValidTypeAsync<Item>(ItemPriceDTO.ItemId);
            if (ItemPriceDTO.UnitCost < 0)
            {
                throw new Exception("There is negative values");
            }

            if( await _ItemsPricesRepository.IsUsedAsync(e => e.ItemId == ItemPriceDTO.ItemId && e.ItemPriceUnitCost == ItemPriceDTO.UnitCost))
            {
                throw new Exception("This Item has This Price befor");
            }

            return ToDTO(await _ItemsPricesRepository.AddAsync(ToEntity(ItemPriceDTO)));

        }

        public async Task<ItemPriceDTO> UpdateItemPriceAsync(ItemPriceDTO ItemPriceDTO)
        {
            var itemPrice = await _ItemsPricesRepository.GetByIdAsync(ItemPriceDTO.Id);

            await _ItemsPricesRepository.IsIdValidTypeAsync<Item>(ItemPriceDTO.ItemId);

            if (ItemPriceDTO.ItemId != itemPrice.ItemId || ItemPriceDTO.UnitCost != itemPrice.ItemPriceUnitCost || ItemPriceDTO.CurrentStock != itemPrice.ItemPriceCurrentStock)
            { 
                throw new Exception("You can not update this value from here");
            }

            itemPrice.ItemPriceCreatedAt = ItemPriceDTO.CreatedAt;


            return ToDTO(await _ItemsPricesRepository.UpdateAsync(itemPrice));
        }

        public async Task DeleteItemPriceAsync(int id)
        {
            var ItemPrice =await _ItemsPricesRepository .GetByIdAsync(id);

            if (ItemPrice.ItemPriceCurrentStock > 0) 
            {
                throw new Exception("Thir are Stock For this price ");
            }

            await _ItemsPricesRepository.DeleteByIdAsync(id);
        }

        public async Task<ItemPriceDTO> RestoreItemPriceAsync(int id)
        {
            return ToDTO(await _ItemsPricesRepository.RestoreAsync(e => e.ItemPriceId == id));
        }

        public static ItemPriceDTO ToDTO(ItemPrice ItemPrice)
        {
            return new ItemPriceDTO
            {
                Id = ItemPrice.ItemPriceId,
                ItemId = ItemPrice.ItemId,
                UnitCost = ItemPrice.ItemPriceUnitCost,
                CurrentStock = ItemPrice.ItemPriceCurrentStock,
                CreatedAt = ItemPrice.ItemPriceCreatedAt
            };
        }

        public static ItemPrice ToEntity(ItemPriceDTO dto)
        {
            return new ItemPrice
            {
                ItemPriceId = dto.Id,
                ItemId = dto.ItemId,
                ItemPriceUnitCost = dto.UnitCost,
                ItemPriceCurrentStock = dto.CurrentStock,
                ItemPriceCreatedAt = dto.CreatedAt,
                IsActive = true
            };
        }
    }
}
