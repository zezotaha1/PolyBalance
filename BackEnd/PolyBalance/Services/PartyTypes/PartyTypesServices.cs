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
            return ToDTO(partyType);
        }

        // Get All PartyTypes
        public async Task<ICollection<PartyTypeDTO>> GetAllPartyTypesAsync()
        {
            var partytypesFromDatabase = await _repository.GetAllAsync();
            var partytypes = new List<PartyTypeDTO>();
            foreach (var pt in partytypesFromDatabase)
            {
                partytypes.Add(ToDTO(pt));
            }
            return partytypes;
        }

        // Create PartyType
        public async Task CreatePartyTypeAsync(PartyTypeDTO partyTypeDTO)
        {
            if (await _validation.NameValidationAsync(partyTypeDTO.Name))
            {
                if(await _repository.FindAsync(e=>e.PartyTypeName==partyTypeDTO.Name)!=null)
                { 
                    throw new InvalidOperationException("This name has already been used.");
                }
                partyTypeDTO.Id = 0;
                await _repository.AddAsync(ToEntity(partyTypeDTO));
            }

        }

        // Update PartyType
        public async Task UpdatePartyTypeAsync( PartyTypeDTO partyTypeDTO)
        {
            var partyType = await _repository.GetByIdAsync(partyTypeDTO.Id);
            await _validation.NameValidationAsync(partyTypeDTO.Name);

            if (await _repository.FindAsync(e => e.PartyTypeName == partyTypeDTO.Name) != null)
            {
                throw new InvalidOperationException("This name has already been used.");
            }

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

        public static PartyTypeDTO ToDTO(PartyType partyType)
        {
            return new PartyTypeDTO
            {
                Id = partyType.PartyTypeId,
                Name = partyType.PartyTypeName
            };
        }

        public static PartyType ToEntity(PartyTypeDTO dto)
        {
            return new PartyType
            {
                PartyTypeId = dto.Id,
                PartyTypeName = dto.Name,
                IsActive = true
            };
        }
    }
}
