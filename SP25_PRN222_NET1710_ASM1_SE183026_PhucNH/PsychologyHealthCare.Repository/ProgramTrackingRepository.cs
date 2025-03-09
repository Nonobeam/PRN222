using Microsoft.EntityFrameworkCore;
using PsychologyHealthCare.Repository.Base;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.Repository
{
    public class ProgramTrackingRepository : GenericRepository<ProgramTracking>
    {
        public async Task<int> CreateProgramTrackingAsync(ProgramTracking programTracking)
        {
            _context.ProgramTrackings.Add(programTracking);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProgramTracking?> GetProgramTrackingByIdAsync(string id)
        {
            return await _context.ProgramTrackings
                .FirstOrDefaultAsync(p => p.Id == id && p.SystemStatus == "ACTIVE");
        }

        public async Task<List<ProgramTracking>> GetAllProgramTrackingsAsync()
        {
            return await _context.ProgramTrackings
                .Where(p => p.SystemStatus == "ACTIVE")
                .ToListAsync();
        }

        public async Task<int> UpdateProgramTrackingAsync(ProgramTracking programTracking)
        {
            var existingProgram = await _context.ProgramTrackings.FindAsync(programTracking.Id);
            if (existingProgram != null && existingProgram.SystemStatus == "ACTIVE")
            {
                _context.Entry(existingProgram).CurrentValues.SetValues(programTracking);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}