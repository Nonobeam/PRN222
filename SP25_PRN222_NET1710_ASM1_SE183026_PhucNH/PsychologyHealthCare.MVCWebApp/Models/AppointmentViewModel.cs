using PsychologyHealthCare.Repository.Models;

namespace PsychologyHealthCare.MVCWebApp.Models
{
    public class AppointmentViewModel
    {
        public IEnumerable<AppointmentTracking> Appointments { get; set; } = [];
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string ?Name { get; set; }
        public string ?Rating { get; set; }
        public string ?Address { get; set; }
    }
}
