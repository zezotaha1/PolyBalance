using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using PolyBalance.Services.Stores;
using System.Data;
using System.Data.SqlTypes;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoresServices _StoresServices;

        public StoreController(IStoresServices partiesServices)
        {
            _StoresServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _StoresServices.GetStoreByIdAsync(Id);
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
                var stores = await _StoresServices.GetAllStoresAsync();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreDTO Store)
        {
            if (Store == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _StoresServices.CreateStoreAsync(Store));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(StoreDTO Store)
        {
            if (Store == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _StoresServices.UpdateStoreAsync(Store));
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
                await _StoresServices.DeleteStoreAsync(id);
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
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                return Ok(await _StoresServices.RestoreStoreAsync(id));
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
