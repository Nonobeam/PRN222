using PsychologyHealthCare.Repository;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.Service
{
    public interface IAppointmentTrackingService
    {
        Task<List<AppointmentTracking>> GetAllAsync();
        Task<AppointmentTracking?> GetById(string id);
        Task<int> Create(AppointmentTracking appointmentTracking);
        Task<int> Update(AppointmentTracking appointmentTracking);
        Task<bool> Delete(AppointmentTracking appointment);
        Task<List<AppointmentTracking>> Search(string name, string status, string address);
    }

    public class AppointmentTrackingService : IAppointmentTrackingService
    {
        private readonly AppointmentTrackingRepository _repository = new();

        public async Task<List<AppointmentTracking>> GetAllAsync()
        {
            return await _repository.GetAllAppointmentTrackingsAsync();
        }

        public async Task<AppointmentTracking?> GetById(string id)
        {
            return await _repository.GetAppointmentTrackingByIdAsync(id);
        }

        public async Task<List<AppointmentTracking>> Search(string name, string rating, string address)
        {
            return await _repository.Search(name, rating, address);
        }

        public async Task<int> Create(AppointmentTracking appointmentTracking) => await _repository.CreateAppointmentTrackingAsync(appointmentTracking);

        public async Task<int> Update(AppointmentTracking appointmentTracking) => await _repository.UpdateAsync(appointmentTracking);

        public async Task<bool> Delete(AppointmentTracking appointment) => await _repository.RemoveAsync(appointment);
    }
}