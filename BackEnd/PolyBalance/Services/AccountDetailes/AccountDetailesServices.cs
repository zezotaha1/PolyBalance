using PolyBalance.DTO;
using PolyBalance.Models;

namespace PolyBalance.Services.AccountDetailes
{
    public class AccountDetailesServices : IAccountDetailesServices
    {
        private readonly Validation _validation;
        private readonly IRepository<AccountDetail> _AccountDetailRepository;


        public AccountDetailesServices(IRepository<AccountDetail> repository, Validation validation)
        {
            _AccountDetailRepository = repository;
            _validation = validation;
        }

        public async Task<AccountDetailDTO> GetAccountDetailByIdAsync(int id)
        {
            var AccountDetail = await _AccountDetailRepository.GetByIdAsync(id); ;

            return ToDTO(AccountDetail);
        }

        public async Task<AccountDetailDTO> GetAccountDetailByOrderIdAsync(int id)
        {
            var AccountDetail = await _AccountDetailRepository.FindAsync(e => e.OrderId == id);

            return ToDTO(AccountDetail.ElementAt(0));
        }

        public async Task<ICollection<AccountDetailDTO>> GetAllAccountDetailsAsync()
        {
            var PartiesFromDatabase = await _AccountDetailRepository.GetAllAsync();
            var Parties = new List<AccountDetailDTO>();
            foreach (var AccountDetail in PartiesFromDatabase)
            {
                Parties.Add(ToDTO(AccountDetail));
            }
            return Parties;
        }
        public async Task<ICollection<AccountDetailDTO>> GetAllAccountDetailsByPartyIDAsync(int Id)
        {
            var PartiesFromDatabase = await _AccountDetailRepository.FindAsync(e => e.PartyId == Id);
            var Parties = new List<AccountDetailDTO>();
            foreach (var AccountDetail in PartiesFromDatabase)
            {
                Parties.Add(ToDTO(AccountDetail));
            }
            return Parties;
        }

        public async Task<AccountDetailDTO> CreateAccountDetailAsync(AccountDetailDTO AccountDetailDTO)
        {
            AccountDetailDTO.Id = 0;

            Validate(AccountDetailDTO);

            if (AccountDetailDTO.CreatedAt == null)
            {
                AccountDetailDTO.CreatedAt = DateTime.Now.ToLocalTime();
            }
            return ToDTO(await _AccountDetailRepository.AddAsync(ToEntity(AccountDetailDTO)));
        }

        public async Task<AccountDetailDTO> UpdateAccountDetailAsync(AccountDetailDTO AccountDetailDTO)
        {
            var AccountDetail = await _AccountDetailRepository.GetByIdAsync(AccountDetailDTO.Id);

            Validate(AccountDetailDTO);

            AccountDetail.PartyId = AccountDetailDTO.PartyId;
            AccountDetail.AccountDetailType = AccountDetailDTO.Type;
            AccountDetail.OrderId = AccountDetailDTO.OrderId;
            AccountDetail.AccountDetailAmount = AccountDetailDTO.Amount;
            AccountDetail.AccountDetailCreatedAt = AccountDetailDTO.CreatedAt;
            AccountDetail.AccountDetailNote = AccountDetailDTO.Note;
            AccountDetail.IsActive = true;

            return ToDTO(await _AccountDetailRepository.UpdateAsync(AccountDetail));
        }

        public async Task DeleteAccountDetailAsync(int id)
        {
            var accountDetail = await GetAccountDetailByIdAsync(id);
            if(accountDetail.OrderId!=null)
            {
                throw new Exception("You can not delete this Detail");
            }
            await _AccountDetailRepository.DeleteByIdAsync(id);
        }

        public async Task<AccountDetailDTO> RestoreAccountDetailAsync(int Id)
        {
            return ToDTO(await _AccountDetailRepository.RestoreAsync(entity => entity.AccountDetailId == Id));
        }

        private void Validate(AccountDetailDTO AccountDetailDTO)
        {
            if (!_AccountDetailRepository.IsIdValidTypeAsync<Party>(AccountDetailDTO.PartyId).Result)
            {
                throw new Exception("This Party Id is not valid");
            }
            if (AccountDetailDTO.Type < 0 || AccountDetailDTO.Type > 2)
            {
                throw new Exception("This is not valid Opration");
            }
            if (AccountDetailDTO.Amount <= 0)
            {
                throw new Exception("This is not valid Amount");
            }
        }

        private static AccountDetailDTO ToDTO(AccountDetail AccountDetail)
        {
            return new AccountDetailDTO
            {
                Id = AccountDetail.AccountDetailId,
                PartyId = AccountDetail.PartyId,
                Type = AccountDetail.AccountDetailType,
                OrderId = AccountDetail.OrderId,
                Amount = AccountDetail.AccountDetailAmount,
                CreatedAt = AccountDetail.AccountDetailCreatedAt,
                Note = AccountDetail.AccountDetailNote
            };
        }

        private static AccountDetail ToEntity(AccountDetailDTO dto)
        {
            return new AccountDetail
            {
                AccountDetailId = dto.Id,
                PartyId = dto.PartyId,
                AccountDetailType = dto.Type,
                OrderId = dto.OrderId,
                AccountDetailAmount = dto.Amount,
                AccountDetailCreatedAt = dto.CreatedAt,
                AccountDetailNote = dto.Note,
                IsActive = true
            };
        }
    }
}
