using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;

namespace PolyBalance.Services
{
    public class Validation
    {
        private readonly PolyBalanceDbContext _polyBalanceDbContext;

        public Validation(PolyBalanceDbContext polyBalanceDbContext)
        {
            _polyBalanceDbContext = polyBalanceDbContext;
        }

        // Validate the entire PartyDTO
        public async Task<bool> ValidPartyAsync(PartyDTO party)
        {
            return await NameValidationAsync(party.Name) && await PhoneNumberValidationAsync(party.PhoneNumber)&& await IsIdValidType<PartyType>(party.TypeId);
        }

        // Validate the name
        public async Task<bool> NameValidationAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            if (name.Length < 3 || name.Length > 50)
            {
                throw new ArgumentException("Name must be between 3 and 50 characters long.");
            }

            return true;
        }

        // Validate the phone number
        public async Task<bool> PhoneNumberValidationAsync(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Phone number cannot be empty.");
            }

            if (number.Length != 11 || !number.StartsWith("01"))
            {
                throw new ArgumentException("Phone number must be 11 digits and start with '01'.");
            }

            var validSecondDigit = new[] { '0', '1', '2', '5' };
            if (!validSecondDigit.Contains(number[2]))
            {
                throw new ArgumentException("Phone number's third digit must be 0, 1, 2, or 5.");
            }

            if (!number.Skip(3).All(char.IsDigit))
            {
                throw new ArgumentException("Phone number must contain only numeric characters.");
            }

            var isNumberUsed = await _polyBalanceDbContext.Parties.AnyAsync(e => e.PartyPhoneNumber == number);
            if (isNumberUsed)
            {
                throw new InvalidOperationException("This phone number has already been used.");
            }

            return true;
        }
        public async Task<bool> IsIdValidType<T>(int id) where T : class
        {
            var entity =await _polyBalanceDbContext.Set<T>().FindAsync(id);
            if( entity == null)
            {
                throw new InvalidOperationException("This Type is not existed");
            }
            return true;
        }

    }
}
