using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service
{
    public class StoreAccountService
    {
        private readonly StoreAccountRepository _repository;
        public StoreAccountService() => _repository = new StoreAccountRepository();

        public async Task<StoreAccount?> Authenticate(string username, string password)
        {
            return await _repository.GetUserAccountAsync(username, password);
        }
    }
}
