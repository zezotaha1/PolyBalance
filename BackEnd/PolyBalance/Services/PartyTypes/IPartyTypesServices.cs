using PolyBalance.DTO;

namespace PolyBalance.Services.PartyTypes
{
    public interface IPartyTypesServices
    {
        public Task<PartyTypeDTO> GetPartyTypeByIdAsync(int id);
        public Task<ICollection<PartyTypeDTO>> GetAllPartyTypesAsync();
        public Task<PartyTypeDTO> CreatePartyTypeAsync(PartyTypeDTO PartyTypeDTO);
        public Task<PartyTypeDTO> UpdatePartyTypeAsync(PartyTypeDTO PartyTypeDTO);
        public Task DeletePartyTypeAsync(int id);
        public Task<PartyTypeDTO> RestorePartyTypeAsync(int id);
    }
}

