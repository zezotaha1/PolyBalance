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

            return new PartyDTO { 
                Id = party.PartyId, 
                Name =party.PartyName,
                TypeId = party.PartyTypeId,
                Amount =party.PartyTotalAmount, 
                PhoneNumber = party.PartyPhoneNumber, 
                Address=party.PartyAddress ?? "" 
            };
        }

        public async Task<ICollection<PartyDTO>> GetAllPartiesAsync()
        {
            var PartiesFromDatabase = await _repositoryParty.GetAllAsync();
            var Parties =new List<PartyDTO>();
            foreach(var party in PartiesFromDatabase)
            {
                Parties.Add(new PartyDTO 
                { 
                    Id = party.PartyId, 
                    Name = party.PartyName,
                    TypeId = party.PartyTypeId, 
                    Amount = party.PartyTotalAmount,
                    PhoneNumber = party.PartyPhoneNumber, 
                    Address = party.PartyAddress 
                });
            }
            return Parties;
        }

        public async Task CreatePartyAsync(PartyDTO partyDTO)
        {
            await _validation.Valid(partyDTO);
            await ValidPartyTypeAsync(partyDTO.TypeId);

            var PartyToDatabade = new Party
            {
                PartyId = 0,
                PartyName = partyDTO.Name,
                PartyTypeId = partyDTO.TypeId,
                PartyPhoneNumber = partyDTO.PhoneNumber,
                PartyAddress = partyDTO.Address,
                PartyTotalAmount = 0,
                IsActive = true
            };
            await _repositoryParty.AddAsync(PartyToDatabade);
        }

        public async Task UpdatePartyAsync(PartyDTO partyDTO) 
        {
            var party = await _repositoryParty.GetByIdAsync(partyDTO.Id);
            await ValidPartyTypeAsync(partyDTO.TypeId);
            await _validation.Valid(partyDTO);

            party.PartyName = partyDTO.Name;
            party.PartyTypeId = partyDTO.TypeId;
            party.PartyPhoneNumber = partyDTO.PhoneNumber;
            party.PartyAddress = partyDTO.Address;

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

        public async Task ValidPartyTypeAsync(int TypeId)
        {
            try
            {
                await _repositoryPartyType.GetByIdAsync(TypeId);
            }
            catch
            {
                throw new Exception("Invalid Type");
            }
        }
    }
}
