using PolyBalance.DTO;

namespace PolyBalance.Services.Orders
{
    public interface IOrdersServices
    {
        public Task<OrderDTO> GetOrderByIdAsync(int id);
        public Task<ICollection<OrderDTO>> GetAllOrdersAsync();
        public Task<OrderDTO> CreateOrderAsync(OrderDTO OrderDTO);
        public Task<OrderDTO> UpdateOrderAsync(OrderDTO OrderDTO);
        public Task DeleteOrderAsync(int id);
        public Task<OrderDTO> RestoreOrderAsync(int id);
    }
}
