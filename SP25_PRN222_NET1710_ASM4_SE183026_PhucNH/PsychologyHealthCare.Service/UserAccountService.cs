using PsychologyHealthCare.Repository;
using PsychologyHealthCare.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsychologyHealthCare.Service
{
    public class UserAccountService
    {
        private readonly UserAccountRepository _repository;

        public UserAccountService() => _repository = new UserAccountRepository();

        public async Task<UserAccount?> Authenticate(string username, string password)
        {
            return await _repository.GetUserAccountAsync(username, password);
        }
    }
}