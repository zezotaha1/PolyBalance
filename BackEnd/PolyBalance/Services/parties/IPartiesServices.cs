using PolyBalance.DTO;

namespace PolyBalance.Services.parties
{
    public interface IPartiesServices
    {
        public Task<PartyDTO> GetPartyByIdAsync(int id);
        public Task<ICollection<PartyDTO>> GetAllPartiesAsync();
        public Task CreatePartyAsync(PartyDTO partyDTO);
        public Task UpdatePartyAsync(PartyDTO partyDTO);
        public Task DeletePartyAsync(int id);
        public  Task RestorePartyAsync(string PhoneNumber);
    }
}
 