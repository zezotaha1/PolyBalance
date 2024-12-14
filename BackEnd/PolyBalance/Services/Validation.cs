using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;
using System.Data.SqlTypes;
using System.Data;

namespace PolyBalance.Services
{
    public class Validation(PolyBalanceDbContext polyBalanceDbcontext)
    {
        private readonly PolyBalanceDbContext _polyBalanceDbcontext = polyBalanceDbcontext;

        public async Task<bool> Valid(PartyDTO party)
        {
            return NameValidation(party.Name) && await PhoneNumberValidationAsync(party.PhoneNumber);
        }
        public bool NameValidation(string Name)
        {
            if (Name.Length < 3 || Name.Length > 50)
                throw new Exception("Invalid Name");

            return true;
        }

        public async Task<bool> PhoneNumberValidationAsync(string Number)
        {
            if (Number.Length == 11 && Number[..2] == "01" && (Number[2] == '0' || Number[2] == '1' || Number[2] == '2' || Number[2] == '5') )
            {
                if (await _polyBalanceDbcontext.Parties.AnyAsync(e => e.PartyPhoneNumber == Number))
                {
                    throw new Exception("This Number is used before");
                }
                return true;
            }
            throw new Exception("Invalid Number");
        }
    }
}
