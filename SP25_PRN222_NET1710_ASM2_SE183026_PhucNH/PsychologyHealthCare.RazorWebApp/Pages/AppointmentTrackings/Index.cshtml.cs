using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.Service;
using PsychologyHealthCare.Repository.Models;


namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppointmentTrackingService _appointmentService;

        public IndexModel(IAppointmentTrackingService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IList<AppointmentTracking> AppointmentTracking { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AppointmentTracking = await _appointmentService.GetAllAsync();
        }
    }
}
