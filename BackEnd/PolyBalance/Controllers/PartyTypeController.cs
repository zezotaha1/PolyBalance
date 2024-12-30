using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.Services.parties;
using PolyBalance.Services.PartyTypes;
using System.Data.SqlTypes;
using System.Data;
using PolyBalance.DTO;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartyTypeController : ControllerBase
    {
        private readonly IPartyTypesServices _PartyTypesServices;

        public PartyTypeController(IPartyTypesServices partiesServices)
        {
            _PartyTypesServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _PartyTypesServices.GetPartyTypeByIdAsync(Id);
                return Ok(party);
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

        [HttpGet]
        public async Task<ICollection<PartyTypeDTO>> GetAllElements()
        {
            return await _PartyTypesServices.GetAllPartyTypesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartyTypeDTO partyType)
        {
            if (partyType == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                await _PartyTypesServices.CreatePartyTypeAsync(partyType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(PartyTypeDTO partyType)
        {
            if (partyType == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                await _PartyTypesServices.UpdatePartyTypeAsync(partyType);
                return Ok();
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
                await _PartyTypesServices.DeletePartyTypeAsync(id);
                return Ok();
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
                await _PartyTypesServices.RestorePartyTypeAsync(id);
                return Ok();
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
