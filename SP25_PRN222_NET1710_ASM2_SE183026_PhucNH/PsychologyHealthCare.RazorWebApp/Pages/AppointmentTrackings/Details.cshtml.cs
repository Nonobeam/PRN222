using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentTrackingService _appopinmentService;
        private ProgramTrackingService _programTrackingService;

        public DetailsModel(IAppointmentTrackingService appopinmentService, ProgramTrackingService programTrackingService)
        {
            _appopinmentService = appopinmentService;
            this._programTrackingService = programTrackingService;
        }

        public AppointmentTracking AppointmentTracking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appopinmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                var program = await _programTrackingService.GetById(appointment.ProgramTrackingId);
                ViewData["ProgramName"] = program?.Name;
                AppointmentTracking = appointment;
            }
            return Page();
        }
    }
}
