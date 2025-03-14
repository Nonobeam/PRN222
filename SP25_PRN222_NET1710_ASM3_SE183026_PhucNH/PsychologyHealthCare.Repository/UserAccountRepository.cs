using Microsoft.EntityFrameworkCore;
using PsychologyHealthCare.Repository.Base;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.Repository
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository() { }

        public async Task<UserAccount?> GetUserAccountAsync(string userName, string password)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive);
        }
    }
}