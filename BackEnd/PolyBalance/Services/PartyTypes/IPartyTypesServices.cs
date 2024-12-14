using PolyBalance.DTO;

namespace PolyBalance.Services.PartyTypes
{
    public interface IPartyTypesServices
    {
        public Task<PartyTypeDTO> GetPartyTypeByIdAsync(int id);
        public Task<ICollection<PartyTypeDTO>> GetAllPartyTypesAsync();
        public Task CreatePartyTypeAsync(PartyTypeDTO PartyTypeDTO);
        public Task UpdatePartyTypeAsync(PartyTypeDTO PartyTypeDTO);
        public Task DeletePartyTypeAsync(int id);
        public Task RestorePartyTypeAsync(int id);
    }
}

