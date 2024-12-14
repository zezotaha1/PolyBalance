using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;

namespace PolyBalance.Services.PartyTypes
{
    public class PartyTypesServices : IPartyTypesServices
    {
        private readonly IRepository<PartyType> _repository;
        private readonly Validation _validation;
        public PartyTypesServices(IRepository<PartyType> Repository, Validation validation)
        {
            
            _repository = Repository;
            _validation = validation;
        }

        // Get PartyType By Id
        public async Task<PartyTypeDTO> GetPartyTypeByIdAsync(int id)
        {
            var partyType = await _repository.GetByIdAsync(id);
            return new PartyTypeDTO
            {
                Id = partyType.PartyTypeId,
                Name = partyType.PartyTypeName
            };
        }

        // Get All PartyTypes
        public async Task<ICollection<PartyTypeDTO>> GetAllPartyTypesAsync()
        {
            var partytypesFromDatabase = await _repository.GetAllAsync();
            var partytypes = new List<PartyTypeDTO>();
            foreach (var pt in partytypesFromDatabase)
            {
                partytypes.Add(new PartyTypeDTO
                {
                    Id = pt.PartyTypeId,
                    Name = pt.PartyTypeName
                });
            }
            return partytypes;
        }

        // Create PartyType
        public async Task CreatePartyTypeAsync(PartyTypeDTO partyTypeDTO)
        {
            if (_validation.NameValidation(partyTypeDTO.Name))
            {

                var partyType = new PartyType
                {
                    PartyTypeId = 0,
                    PartyTypeName = partyTypeDTO.Name,
                    IsActive = true
                };
                await _repository.AddAsync(partyType);
            }

        }

        // Update PartyType
        public async Task UpdatePartyTypeAsync( PartyTypeDTO partyTypeDTO)
        {
            var partyType = await _repository.GetByIdAsync(partyTypeDTO.Id);
            _validation.NameValidation(partyTypeDTO.Name);
            
            partyType.PartyTypeName = partyTypeDTO.Name;
            await _repository.UpdateAsync(partyType);
            
        }

        // Delete PartyType (Soft Delete)
        public async Task DeletePartyTypeAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        // Restore PartyType
        public async Task RestorePartyTypeAsync(int id)
        {
            await _repository.RestoreAsync(entity => entity.PartyTypeId == id);
        }
    }
}
