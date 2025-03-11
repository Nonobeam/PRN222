using Microsoft.EntityFrameworkCore;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PsychologyHealthCare.Repository.Base;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository
{
    public class ManufacturesRepository : GenericRepository<Manufacturer>
    {
        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _context.Manufacturers
                .ToListAsync();
        }
    }
}
