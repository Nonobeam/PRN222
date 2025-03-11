using Microsoft.EntityFrameworkCore;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PsychologyHealthCare.Repository.Base;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository
{
    public class MedicineInformationRepository : GenericRepository<MedicineInformation>
    {
        private readonly int pageSize = 3;

        public async Task<List<MedicineInformation>> GetAllMedicinesAsync()
        {
            return await _context.MedicineInformations
                .Include(m => m.Manufacturer)
                .ToListAsync();
        }

        public async Task<List<MedicineInformation>> GetAllMedicinesAsync(int pageNumber)
        {
            return await _context.MedicineInformations
                .Include(m => m.Manufacturer)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MedicineInformation?> GetByIdAsync(string id)
        {
            return await _context.MedicineInformations
                .Include(m => m.Manufacturer).FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<int> GetTotalCountAsync(string? active, string? expire, string? warn)
        {
            return await _context.MedicineInformations
                .Include(q => q.Manufacturer)
                .Where(q => (q.ActiveIngredients.Contains(active) || q.ExpirationDate.ToString().Contains(expire) || q.WarningsAndPrecautions.Contains(warn)))
                .CountAsync();
        }

        public async Task<List<MedicineInformation>> Search(string active, string expire, string warn, int pageNumber)
        {
            return await _context.MedicineInformations
                .Include(q => q.Manufacturer)
                .Where(q => (q.ActiveIngredients.Contains(active) || q.ExpirationDate.ToString().Contains(expire) || q.WarningsAndPrecautions.Contains(warn)))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
