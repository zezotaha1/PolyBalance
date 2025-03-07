using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PolyBalance.DTO;
using PolyBalance.Models;
using PolyBalance.Repository;
using PolyBalance.Services.AccountDetailes;
using PolyBalance.Services.ItemsPrices;
using PolyBalance.Services.Items;

namespace PolyBalance.Services.Orders
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IAccountDetailesServices _accountDetailesServices;
        private readonly IItemsPricesServices _itemsPricesServices ;
        private readonly IItemsServices _itemsServices;


        public OrdersServices(IRepository<Order> orderRepository, IRepository<OrderDetail> orderDetailRepository, IAccountDetailesServices accountDetailesServices , IItemsPricesServices itemsPricesServices, IItemsServices itemsServices)
        {
            _orderRepository = orderRepository;
            _accountDetailesServices = accountDetailesServices;
            _itemsPricesServices = itemsPricesServices;
            _itemsServices = itemsServices;

        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            return ToDTO(order);
        }

        public async Task<ICollection<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(ToDTO).ToList();
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO)
        {
            await Validate(orderDTO);
            //create item price before create order and return the relation between itemprice and orderDetails 
            if(orderDTO.Type!="Selling")
            {
                for (int i=0; i<orderDTO.OrderDetails.Count;i++)
                {

                    var itemPricesForItem = await _itemsPricesServices.GetAllItemPricesForOneItemAsync(orderDTO.OrderDetails[i].UintId);

                    ItemPriceDTO? x = itemPricesForItem.FirstOrDefault(e=>e.UnitCost== orderDTO.OrderDetails[i].UnitPrice);
                    if (x == null)
                    {
                        x = await _itemsPricesServices.CreateItemPriceAsync(new ItemPriceDTO
                        {
                            ItemId = orderDTO.OrderDetails[i].UintId,
                            UnitCost = orderDTO.OrderDetails[i].UnitPrice,
                            CurrentStock = 0,
                            CreatedAt = orderDTO.Date
                        });
                        orderDTO.OrderDetails[i].UintId = x.Id;
                    }
                    orderDTO.OrderDetails[i].UintId = x.Id;
                }
            }




            //create order
            var order = new Order
            {
                OrderType = (orderDTO.Type == "Selling" ? true : false),
                PartyId = orderDTO.PartyId,
                OrderDate = orderDTO.Date,
                OrderPaid = orderDTO.Paid,
                IsActive = true,
                OrderDetails = orderDTO.OrderDetails.Select(d => new OrderDetail
                {
                    ItemPriceId = d.UintId,
                    OrderDetailQuantity = d.Quantity,
                    OrderDetailUnitPrice = d.UnitPrice,
                    OrderDetailDiscount = d.Discount,
                    IsActive = true
                }).ToList()
            };

            await _orderRepository.AddAsync(order);


            if (order.OrderRemender > 0)
            {
                await _accountDetailesServices.CreateAccountDetailAsync(new AccountDetailDTO
                {
                    PartyId = order.PartyId,
                    CreatedAt = order.OrderDate,
                    OrderId = order.OrderId,
                    Amount = order.OrderRemender,
                    Type = (order.OrderType?0:1)
                
                });
            }


            //add transaction if(paid >0)


            return await GetOrderByIdAsync(order.OrderId);
        }
        public async Task<OrderDTO> UpdateOrderAsync( OrderDTO orderDTO)
        {
            await Validate(orderDTO);
            var order = await _orderRepository.GetByIdAsync(orderDTO.Id);

            order.OrderType = (orderDTO.Type == "Selling" ? true : false);
            order.PartyId = orderDTO.PartyId;
            order.OrderDate = orderDTO.Date;
            order.OrderPaid = orderDTO.Paid;

            foreach (var detailDTO in orderDTO.OrderDetails)
            {
                var existingDetail = order.OrderDetails.FirstOrDefault(d => d.OrderDetailId == detailDTO.Id);
                if (existingDetail != null)
                {
                    existingDetail.ItemPriceId = detailDTO.UintId;
                    existingDetail.OrderDetailQuantity = detailDTO.Quantity;
                    existingDetail.OrderDetailUnitPrice = detailDTO.UnitPrice;
                    existingDetail.OrderDetailDiscount = detailDTO.Discount;
                }
                else
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        ItemPriceId = detailDTO.UintId,
                        OrderDetailQuantity = detailDTO.Quantity,
                        OrderDetailUnitPrice = detailDTO.UnitPrice,
                        OrderDetailDiscount = detailDTO.Discount,
                        IsActive = true
                    });
                }
            }

            await _orderRepository.UpdateAsync(order);

            try
            {
                var accountDetail=await _accountDetailesServices.GetAccountDetailByOrderIdAsync(order.OrderId);
                if(order.OrderRemender!=0)
                {
                    accountDetail.PartyId = order.PartyId;
                    accountDetail.Amount = order.OrderRemender;
                    accountDetail.Type = (order.OrderType ? 0 : 1);
                    await _accountDetailesServices.UpdateAccountDetailAsync(accountDetail);
                }
                else
                {
                    await _accountDetailesServices.DeleteAccountDetailAsync(accountDetail.Id);
                }
            }
            catch
            {
                if (order.OrderRemender != 0)
                {
                    await _accountDetailesServices.CreateAccountDetailAsync(new AccountDetailDTO
                    {
                        PartyId = order.PartyId,
                        CreatedAt = order.OrderDate,
                        OrderId = order.OrderId,
                        Amount = order.OrderRemender,
                        Type = (order.OrderType ? 0 : 1)
                    });
                }
            }
            //add transaction
            return await GetOrderByIdAsync(order.OrderId);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteByIdAsync(id);
            await _accountDetailesServices.DeleteAccountDetailAsync((await _accountDetailesServices.GetAccountDetailByOrderIdAsync(id)).Id);
        }

        public async Task<OrderDTO> RestoreOrderAsync(int id)
        {
            return ToDTO(await _orderRepository.RestoreAsync(e => e.OrderId == id));
        }

        private static OrderDTO ToDTO(Order order)
        {
            return new OrderDTO
            {
                Id = order.OrderId,
                Type = (order.OrderType?"Selling": "Buying"),
                PartyId = order.PartyId,
                Date = order.OrderDate,
                Paid = order.OrderPaid,
                TotalAmount = order.OrderTotalAmount,
                LineTotal = order.OrderLineTotal,
                Remender = order.OrderRemender,
                OrderDetails = order.OrderDetails.Select(d => new OrderDetailDTO
                {
                    Id = d.OrderDetailId,
                    OrderId = d.OrderId,
                    UintId = d.ItemPriceId,
                    Quantity = d.OrderDetailQuantity,
                    UnitPrice = d.OrderDetailUnitPrice,
                    Discount = d.OrderDetailDiscount
                }).ToList()
            };
        }

        private async Task Validate(OrderDTO orderDTO)
        {
            await _orderRepository.IsIdValidTypeAsync<Party>(orderDTO.PartyId);

            if (orderDTO.Paid < 0)
            {
                throw new Exception("Paid amount must be greater than or equal to 0");
            }

            if (orderDTO.OrderDetails == null || orderDTO.OrderDetails.Count == 0)
            {
                throw new Exception("Order must have at least one detail");
            }

            if (orderDTO.OrderDetails.Any(d => d.Quantity <= 0))
            {
                throw new Exception("Quantity must be greater than 0");
            }

            if (orderDTO.OrderDetails.Any(d => d.UnitPrice <= 0))
            {
                throw new Exception("Unit price must be greater than 0");
            }

            if (orderDTO.OrderDetails.Any(d => d.Discount < 0 || d.Discount > 1))
            {
                throw new Exception("Discount must be between 0 and 1");
            }

            var duplicateItems = orderDTO.OrderDetails
                .GroupBy(d => d.UintId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateItems.Any())
            {
                throw new Exception($"The following ItemsPriceId(s) are repeated within the order: {string.Join(", ", duplicateItems)}");
            }
            if (orderDTO.Type == "Selling")
            {
                foreach (var detail in orderDTO.OrderDetails)
                {
                    if ((await _itemsPricesServices.GetItemPriceByIdAsync(detail.UintId)).CurrentStock < detail.Quantity)
                    {
                        throw new Exception("the item price did not have this quantity");
                    }
                }
            }
            else
            {
                foreach (var detail in orderDTO.OrderDetails)
                {
                    await _itemsServices.GetItemByIdAsync(detail.UintId);
                }
            }
        }
    }
}
