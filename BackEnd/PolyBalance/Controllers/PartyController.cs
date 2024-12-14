using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PolyBalance.DTO;
using PolyBalance.Services.parties;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlTypes;

namespace PolyBalance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly IPartiesServices _PartiesServices;

        public PartyController(IPartiesServices partiesServices)
        {
            _PartiesServices = partiesServices;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetElement(int Id)
        {

            try
            {
                var party = await _PartiesServices.GetPartyByIdAsync(Id);
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
        public async Task<ICollection<PartyDTO>> GetAllElements()
        {
            return await _PartiesServices.GetAllPartiesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartyDTO party)
        {
            if (party == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                await _PartiesServices.CreatePartyAsync(party);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(PartyDTO party)
        {
            if (party == null)
            {
                return BadRequest("NO Data");
            }

            try
            {
                await _PartiesServices.UpdatePartyAsync(party);
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
                await _PartiesServices.DeletePartyAsync(id);
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

        [HttpPatch("{Number}")]
        public async Task<IActionResult> Restore(string Number)
        {
            if (string.IsNullOrEmpty(Number))
            {
                return BadRequest("NO Data");
            }

            try
            {
                await _PartiesServices.RestorePartyAsync(Number);
                return Ok();
            }
            catch(SqlNullValueException ex)
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
