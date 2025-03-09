using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentTrackingService _appopinmentService;

        public DetailsModel(IAppointmentTrackingService appopinmentService)
        {
            _appopinmentService = appopinmentService;
        }

        public AppointmentTracking AppointmentTracking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyquest = await _appopinmentService.GetById(id);
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
    }
}
