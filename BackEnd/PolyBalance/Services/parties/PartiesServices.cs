 using Microsoft.EntityFrameworkCore;
using PolyBalance.DTO;
using PolyBalance.Models;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlTypes;
using System.IO;

namespace PolyBalance.Services.parties
{
    public class PartiesServices : IPartiesServices
    {
        
        private readonly Validation _validation;
        private readonly IRepository<Party> _PartyRepository;


        public PartiesServices(IRepository<Party> repository, Validation validation)
        {
            _PartyRepository = repository;
            _validation=validation;
        }

        public async Task<PartyDTO> GetPartyByIdAsync(int id)
        {
            var party =  await _PartyRepository.GetByIdAsync(id);;

            return ToDTO(party);
        }

        public async Task<ICollection<PartyDTO>> GetAllPartiesAsync()
        {
            var PartiesFromDatabase = await _PartyRepository.GetAllAsync();
            var Parties =new List<PartyDTO>();
            foreach(var party in PartiesFromDatabase)
            {
                Parties.Add(ToDTO(party));
            }
            return Parties;
        }

        public async Task<PartyDTO> CreatePartyAsync(PartyDTO partyDTO)
        {
            partyDTO.Id = 0;
            _validation.ValidPartyAsync(partyDTO);
            var isNumberUsed = await _PartyRepository.IsUsedAsync(e => e.PartyPhoneNumber == partyDTO.PhoneNumber);
            if (isNumberUsed)
            {
                throw new InvalidOperationException("This phone number has already been used.");
            }
            await _PartyRepository.IsIdValidTypeAsync<PartyType>(partyDTO.TypeId);
            return ToDTO(await _PartyRepository.AddAsync(ToEntity(partyDTO)));
        }

        public async Task<PartyDTO> UpdatePartyAsync(PartyDTO partyDTO) 
        {
            var party = await _PartyRepository.GetByIdAsync(partyDTO.Id);

            if(partyDTO.Name!= party.PartyName)
            {
                _validation.NameValidationAsync(party.PartyName);
                party.PartyName = partyDTO.Name;
            }
            if(partyDTO.PhoneNumber!= party.PartyPhoneNumber)
            {
                _validation.PhoneNumberValidationAsync(partyDTO.PhoneNumber);
                var isNumberUsed = await _PartyRepository.IsUsedAsync(e => e.PartyPhoneNumber == partyDTO.PhoneNumber);
                if (isNumberUsed)
                {
                    throw new InvalidOperationException("This phone number has already been used.");
                }
                party.PartyPhoneNumber = partyDTO.PhoneNumber;
            }
            if (party.PartyTypeId != partyDTO.TypeId) 
            {
                await _PartyRepository.IsIdValidTypeAsync<PartyType>(partyDTO.TypeId);
                party.PartyTypeId = partyDTO.TypeId;
            }

            party.PartyAddress = partyDTO.Address;
            party.PartyRating = partyDTO.Rating;

            return ToDTO(await _PartyRepository.UpdateAsync(party));
        }

        public async Task DeletePartyAsync(int id)
        {
            await _PartyRepository.DeleteByIdAsync(id);
        }

        public async Task<PartyDTO> RestorePartyAsync(string PhoneNumber)
        {
             _validation.PhoneNumberValidationAsync(PhoneNumber);
             return ToDTO(await _PartyRepository.RestoreAsync(entity=>entity.PartyPhoneNumber == PhoneNumber));
        }

        private static PartyDTO ToDTO(Party party)
        {
            return new PartyDTO
            {
                Id = party.PartyId,
                Name = party.PartyName,
                TypeId = party.PartyTypeId,
                Amount = party.PartyTotalAmount,
                PhoneNumber = party.PartyPhoneNumber,
                Address = party.PartyAddress ?? "",
                Rating = party.PartyRating
            };
        }

        private static Party ToEntity(PartyDTO dto)
        {
            return new Party
            {
                PartyId = dto.Id,
                PartyName = dto.Name,
                PartyTypeId = dto.TypeId,
                PartyPhoneNumber = dto.PhoneNumber,
                PartyAddress = dto.Address,
                PartyRating = dto.Rating,
                PartyTotalAmount = dto.Amount,
                IsActive = true
            };
        }

    }
}
