using Microsoft.EntityFrameworkCore;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PsychologyHealthCare.Repository.Base;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository
{
    public class StoreAccountRepository : GenericRepository<StoreAccount>
    {
        public StoreAccountRepository() { }

        public async Task<StoreAccount?> GetUserAccountAsync(string email, string password)
        {
            return await _context.StoreAccounts.FirstOrDefaultAsync(u => u.EmailAddress == email && u.StoreAccountPassword == password);
        }
    }
}
