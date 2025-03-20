using Microsoft.EntityFrameworkCore;
using PsychologyHealthCare.Repository.Base;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.Repository
{
    public class AppointmentTrackingRepository : GenericRepository<AppointmentTracking>
    {
        public async Task<int> CreateAppointmentTrackingAsync(AppointmentTracking appointmentTracking)
        {
            _context.AppointmentTrackings.Add(appointmentTracking);
            return await _context.SaveChangesAsync();
        }

        public async Task<AppointmentTracking?> GetAppointmentTrackingByIdAsync(string id)
        {
            return await _context.AppointmentTrackings
                .Include(a => a.ProgramTracking)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AppointmentTracking>> GetAllAppointmentTrackingsAsync()
        {
            return await _context.AppointmentTrackings
                .Include(a => a.ProgramTracking)
                .ToListAsync();
        }

        public async Task<List<AppointmentTracking>> Search(string a, string b, string c)
        {
            var trimA = a?.Trim() ?? string.Empty;
            var trimB = b?.Trim() ?? string.Empty;
            var trimC = c?.Trim() ?? string.Empty;
            return await _context.AppointmentTrackings
                .Include(a => a.ProgramTracking)
                .Where(a =>
                    (string.IsNullOrEmpty(trimA) || a.Name.Contains(trimA)) &&
                    (string.IsNullOrEmpty(trimB) || a.Rating.Equals(trimB)) &&
                    (string.IsNullOrEmpty(trimC) || a.Address.Contains(trimC))
                )
                .ToListAsync();
        }
    }
}