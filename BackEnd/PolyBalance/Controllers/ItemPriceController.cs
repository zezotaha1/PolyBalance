using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using PolyBalance.Services.ItemsPrices;
using System.Data.SqlTypes;
using System.Data;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemPriceController : ControllerBase
    {
        private readonly IItemsPricesServices _ItemsPricesServices;

        public ItemPriceController(IItemsPricesServices partiesServices)
        {
            _ItemsPricesServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _ItemsPricesServices.GetItemPriceByIdAsync(Id);
                return Ok(party);
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
                var ItemPrices = await _ItemsPricesServices.GetAllItemPricesAsync();
                return Ok(ItemPrices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemPriceDTO ItemPrice)
        {
            if (ItemPrice == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _ItemsPricesServices.CreateItemPriceAsync(ItemPrice));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ItemPriceDTO ItemPrice)
        {
            if (ItemPrice == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _ItemsPricesServices.UpdateItemPriceAsync(ItemPrice));
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
                await _ItemsPricesServices.DeleteItemPriceAsync(id);
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
        public async Task<IActionResult> ReItemPrice(int id)
        {
            try
            {
                return Ok(await _ItemsPricesServices.RestoreItemPriceAsync(id));
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
