using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using PolyBalance.Services.Items;
using System.Data.SqlTypes;
using System.Data;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemsServices _ItemsServices;

        public ItemController(IItemsServices itemsServices)
        {
            _ItemsServices = itemsServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _ItemsServices.GetItemByIdAsync(Id);
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
                var Items =  await _ItemsServices.GetAllItemsAsync();
                return Ok(Items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemDTO Item)
        {
            if (Item == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _ItemsServices.CreateItemAsync(Item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ItemDTO Item)
        {
            if (Item == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _ItemsServices.UpdateItemAsync(Item));
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
                await _ItemsServices.DeleteItemAsync(id);
                return Ok("Deleted Successfully");
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
        public async Task<IActionResult> RestoreItem(int id)
        {
            try
            {
                return Ok(await _ItemsServices.RestoreItemAsync(id));
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
