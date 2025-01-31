using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using System.Data.SqlTypes;
using System.Data;
using PolyBalance.Services.Orders;
using PolyBalance.Models;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersServices _OrdersServices;

        public OrderController(IOrdersServices partiesServices)
        {
            _OrdersServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var order = await _OrdersServices.GetOrderByIdAsync(Id);
                return Ok(order);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllElements()
        {
            try
            {
                var Orders = await _OrdersServices.GetAllOrdersAsync();
                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO Order)
        {
            if (Order == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _OrdersServices.CreateOrderAsync(Order));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDTO Order)
        {
            if (Order == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _OrdersServices.UpdateOrderAsync(Order));
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DeletedRowInaccessibleException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _OrdersServices.DeleteOrderAsync(id);
                return Ok("DEleted Successfully");
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DeletedRowInaccessibleException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ReOrder(int id)
        {
            try
            {
                return Ok(await _OrdersServices.RestoreOrderAsync(id));
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
