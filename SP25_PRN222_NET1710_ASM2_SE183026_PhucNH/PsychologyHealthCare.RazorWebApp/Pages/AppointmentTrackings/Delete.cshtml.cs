using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;
using PsychologyHeathCare.RazorWebApp.Hubs;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "1, 2")]
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyquest = await _appointmentService.GetById(id);

            if (surveyquest == null)
            {
                return NotFound();
            }
            else
            {
                AppointmentTracking = surveyquest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyquest = await _appointmentService.GetById(id);
            if (surveyquest != null)
            {
                AppointmentTracking = surveyquest;
                await _appointmentService.Delete(surveyquest);
                await _hubContext.Clients.All.SendAsync("Delete_SurveyQuest", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
