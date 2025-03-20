using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;
using PsychologyHeathCare.RazorWebApp.Hubs;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "2")]
    public class DeleteModel : PageModel
    {
        private readonly IAppointmentTrackingService _appointmentService;
        private readonly IHubContext<PsychologyHealthCareHub> _hubContext;

        public DeleteModel(IAppointmentTrackingService appointmentService, IHubContext<PsychologyHealthCareHub> hubContext)
        {
            _appointmentService = appointmentService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public AppointmentTracking AppointmentTracking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentService.GetById(id);

            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                AppointmentTracking = appointment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentService.GetById(id);
            if (appointment != null)
            {
                AppointmentTracking = appointment;
                await _appointmentService.Delete(appointment);
                await _hubContext.Clients.All.SendAsync("Delete_Appointment", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
