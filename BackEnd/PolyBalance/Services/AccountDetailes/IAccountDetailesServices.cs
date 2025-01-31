using PolyBalance.DTO;

namespace PolyBalance.Services.AccountDetailes
{
    public interface IAccountDetailesServices
    {
        public Task<AccountDetailDTO> GetAccountDetailByIdAsync(int id);
        public Task<ICollection<AccountDetailDTO>> GetAllAccountDetailsAsync();
        public Task<ICollection<AccountDetailDTO>> GetAllAccountDetailsByPartyIDAsync(int id);
        public Task<AccountDetailDTO> GetAccountDetailByOrderIdAsync(int id);
        public Task<AccountDetailDTO> CreateAccountDetailAsync(AccountDetailDTO AccountDetailDTO);
        public Task<AccountDetailDTO> UpdateAccountDetailAsync(AccountDetailDTO AccountDetailDTO);
        public Task DeleteAccountDetailAsync(int id);
        public Task<AccountDetailDTO> RestoreAccountDetailAsync(int id);
    }
}
