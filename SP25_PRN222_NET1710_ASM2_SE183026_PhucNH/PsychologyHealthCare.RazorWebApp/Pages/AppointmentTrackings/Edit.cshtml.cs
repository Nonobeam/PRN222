using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAppointmentTrackingService _appointmentService;
        private readonly ProgramTrackingService _programService;
        public EditModel(IAppointmentTrackingService appointmentService, ProgramTrackingService programService)
        {
            _appointmentService = appointmentService;
            _programService = programService;
        }

        [BindProperty]
        public AppointmentTracking AppointmentTracking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
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
            AppointmentTracking = surveyquest;
            ViewData["ProgramId"] = new SelectList(await _programService.GetAllAsync(), "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _appointmentService.Update(AppointmentTracking);
            return RedirectToPage("./Index");
        }
    }
}
