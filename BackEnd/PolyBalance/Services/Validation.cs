using PolyBalance.DTO;

namespace PolyBalance.Services
{
    public class Validation
    {
       
        // Validate the entire PartyDTO
        public bool ValidPartyAsync(PartyDTO party)
        {
            return NameValidationAsync(party.Name) && PhoneNumberValidationAsync(party.PhoneNumber);
        }

        // Validate the name
        public bool NameValidationAsync(string name)
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
        public  bool PhoneNumberValidationAsync(string number)
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

            return true;
        }

    }
}
