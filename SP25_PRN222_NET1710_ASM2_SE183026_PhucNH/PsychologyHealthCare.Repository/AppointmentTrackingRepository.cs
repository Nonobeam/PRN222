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
                .FirstOrDefaultAsync(a => a.Id == id && a.SystemStatus == "ACTIVE");
        }

        public async Task<List<AppointmentTracking>> GetAllAppointmentTrackingsAsync()
        {
            return await _context.AppointmentTrackings
                .Include(a => a.ProgramTracking)
                .Where(a => a.SystemStatus == "ACTIVE")
                .ToListAsync();
        }

        public async Task<List<AppointmentTracking>> Search(string name, string rating, string address)
        {
            var appointments = await _context.AppointmentTrackings
                .Include(a => a.ProgramTracking)
                .Where(a =>
                    (a.Name.Contains(name) || string.IsNullOrEmpty(name)) &&
                    (a.Rating.Equals(rating) || string.IsNullOrEmpty(rating)) &&
                    (a.Address.Contains(address) || string.IsNullOrEmpty(address))
                )
                .ToListAsync();

            return appointments;
        }
    }
}