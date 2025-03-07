using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;

namespace PolyBalance.Services.PartyTypes
{
    public class PartyTypesServices : IPartyTypesServices
    {
        private readonly IRepository<PartyType> _PartyTypeRepository;
        private readonly Validation _validation;
        public PartyTypesServices(IRepository<PartyType> Repository, Validation validation)
        {
            
            _PartyTypeRepository = Repository;
            _validation = validation;
        }

        // Get PartyType By Id
        public async Task<PartyTypeDTO> GetPartyTypeByIdAsync(int id)
        {
            var partyType = await _PartyTypeRepository.GetByIdAsync(id);
            return ToDTO(partyType);
        }

        // Get All PartyTypes
        public async Task<ICollection<PartyTypeDTO>> GetAllPartyTypesAsync()
        {
            var partytypesFromDatabase = await _PartyTypeRepository.GetAllAsync();
            var partytypes = new List<PartyTypeDTO>();
            foreach (var pt in partytypesFromDatabase)
            {
                partytypes.Add(ToDTO(pt));
            }
            return partytypes;
        }

        // Create PartyType
        public async Task<PartyTypeDTO> CreatePartyTypeAsync(PartyTypeDTO partyTypeDTO)
        {
            _validation.NameValidationAsync(partyTypeDTO.Name);
            if (await _PartyTypeRepository.IsUsedAsync(e=>e.PartyTypeName== partyTypeDTO.Name))
            { 
                throw new InvalidOperationException("This name has already been used.");
            }
            partyTypeDTO.Id = 0;
            return ToDTO(await _PartyTypeRepository.AddAsync(ToEntity(partyTypeDTO)));

        }

        // Update PartyType
        public async Task<PartyTypeDTO> UpdatePartyTypeAsync( PartyTypeDTO partyTypeDTO)
        {
            var partyType = await _PartyTypeRepository.GetByIdAsync(partyTypeDTO.Id);

            _validation.NameValidationAsync(partyTypeDTO.Name);

            if (await _PartyTypeRepository.IsUsedAsync(e => e.PartyTypeName == partyTypeDTO.Name))
            {
                throw new InvalidOperationException("This name has already been used.");
            }

            partyType.PartyTypeName = partyTypeDTO.Name;
            return ToDTO(await _PartyTypeRepository.UpdateAsync(partyType));
            
        }

        // Delete PartyType (Soft Delete)
        public async Task DeletePartyTypeAsync(int id)
        {
            var count = _PartyTypeRepository.GetByIdAsync(id).Result.Parties.Count();

            if(count>0)
            {
                throw new Exception("You can not delete this Type ,Becouse thir are party included");
            }

            await _PartyTypeRepository.DeleteByIdAsync(id);
        }

        // Restore PartyType
        public async Task<PartyTypeDTO> RestorePartyTypeAsync(int id)
        {
            return ToDTO(await _PartyTypeRepository.RestoreAsync(entity => entity.PartyTypeId == id));
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
