using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IAppointmentTrackingService _appointmentService;
        private readonly ProgramTrackingService _programService;
        public CreateModel(IAppointmentTrackingService appointmentService, ProgramTrackingService programService)
        {
            _appointmentService = appointmentService;
            _programService = programService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["ProgramId"] = new SelectList(await _programService.GetAllAsync(), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public AppointmentTracking AppointmentTracking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _appointmentService.Create(AppointmentTracking);

            return RedirectToPage("./Index");
        }
    }
}
