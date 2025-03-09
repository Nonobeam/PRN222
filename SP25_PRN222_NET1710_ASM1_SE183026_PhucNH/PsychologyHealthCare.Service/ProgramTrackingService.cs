using PsychologyHealthCare.Repository;
using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.Service
{
    public class ProgramTrackingService
    {
        private readonly ProgramTrackingRepository _repository = new();

        public async Task<List<ProgramTracking>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
