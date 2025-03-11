using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service
{
    public class ManufacturesService
    {
        private readonly ManufacturesRepository _repository = new();

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Manufacturer> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
