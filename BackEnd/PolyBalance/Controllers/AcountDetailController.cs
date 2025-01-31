using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using PolyBalance.Services.AccountDetailes;
using System.Data.SqlTypes;
using System.Data;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AcountDetailController : ControllerBase
    {
        private readonly IAccountDetailesServices _AccountDetailsServices;

        public AcountDetailController(IAccountDetailesServices partiesServices)
        {
            _AccountDetailsServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _AccountDetailsServices.GetAccountDetailByIdAsync(Id);
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
                var AccountDetails = await _AccountDetailsServices.GetAllAccountDetailsAsync();
                return Ok(AccountDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getElementsForParty")]
        public async Task<IActionResult> GetAllElementsForParty(int id)
        {
            try
            {
                var AccountDetails = await _AccountDetailsServices.GetAllAccountDetailsByPartyIDAsync(id);
                return Ok(AccountDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountDetailDTO AccountDetail)
        {
            if (AccountDetail == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _AccountDetailsServices.CreateAccountDetailAsync(AccountDetail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountDetailDTO AccountDetail)
        {
            if (AccountDetail == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                return Ok(await _AccountDetailsServices.UpdateAccountDetailAsync(AccountDetail));
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
                await _AccountDetailsServices.DeleteAccountDetailAsync(id);
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
        public async Task<IActionResult> ReAccountDetail(int id)
        {
            try
            {
                return Ok(await _AccountDetailsServices.RestoreAccountDetailAsync(id));
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
