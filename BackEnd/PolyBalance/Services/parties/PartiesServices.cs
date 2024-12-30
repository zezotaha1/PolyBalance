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
        private readonly IRepository<Party> _repositoryParty;
        private readonly IRepository<PartyType> _repositoryPartyType;


        public PartiesServices(IRepository<Party> repository, Validation validation, IRepository<PartyType> repositoryPartyType)
        {
            _repositoryParty = repository;
            _validation=validation;
            _repositoryPartyType = repositoryPartyType;
        }

        public async Task<PartyDTO> GetPartyByIdAsync(int id)
        {
            var party =  await _repositoryParty.GetByIdAsync(id);;

            return ToDTO(party);
        }

        public async Task<ICollection<PartyDTO>> GetAllPartiesAsync()
        {
            var PartiesFromDatabase = await _repositoryParty.GetAllAsync();
            var Parties =new List<PartyDTO>();
            foreach(var party in PartiesFromDatabase)
            {
                Parties.Add(ToDTO(party));
            }
            return Parties;
        }

        public async Task CreatePartyAsync(PartyDTO partyDTO)
        {
            partyDTO.Id = 0;
            await _validation.ValidPartyAsync(partyDTO);

            await _repositoryParty.AddAsync(ToEntity(partyDTO));
        }

        public async Task UpdatePartyAsync(PartyDTO partyDTO) 
        {
            var party = await _repositoryParty.GetByIdAsync(partyDTO.Id);

            if(partyDTO.Name!= party.PartyName)
            {
                await _validation.NameValidationAsync(party.PartyName);
                party.PartyName = partyDTO.Name;
            }
            if(partyDTO.PhoneNumber!= party.PartyPhoneNumber)
            {
                await _validation.PhoneNumberValidationAsync(partyDTO.PhoneNumber);
                party.PartyPhoneNumber = partyDTO.PhoneNumber;
            }
            if (party.PartyTypeId != partyDTO.TypeId) 
            {
                await _validation.IsIdValidType<PartyType>(partyDTO.TypeId);
                party.PartyTypeId = partyDTO.TypeId;
            }

            party.PartyAddress = partyDTO.Address;
            party.PartyRateing = partyDTO.Rateing;

            await _repositoryParty.UpdateAsync(party);
        }

        public async Task DeletePartyAsync(int id)
        {
            await _repositoryParty.DeleteByIdAsync(id);
        }

        public async Task RestorePartyAsync(string PhoneNumber)
        {
            if (await _validation.PhoneNumberValidationAsync(PhoneNumber))
            {
                await _repositoryParty.RestoreAsync(entity=>entity.PartyPhoneNumber == PhoneNumber);
            }
        }

        public static PartyDTO ToDTO(Party party)
        {
            return new PartyDTO
            {
                Id = party.PartyId,
                Name = party.PartyName,
                TypeId = party.PartyTypeId,
                Amount = party.PartyTotalAmount,
                PhoneNumber = party.PartyPhoneNumber,
                Address = party.PartyAddress ?? "",
                Rateing = party.PartyRateing
            };
        }

        public static Party ToEntity(PartyDTO dto)
        {
            return new Party
            {
                PartyId = dto.Id,
                PartyName = dto.Name,
                PartyTypeId = dto.TypeId,
                PartyPhoneNumber = dto.PhoneNumber,
                PartyAddress = dto.Address,
                PartyRateing = dto.Rateing,
                PartyTotalAmount = dto.Amount,
                IsActive = true
            };
        }

    }
}
