using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;
using PsychologyHeathCare.RazorWebApp.Hubs;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "2")]
    public class DeleteModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly IHubContext<PharmaHub> _hubContext;

        public DeleteModel(IMedicineService medicineService, IHubContext<PharmaHub> hubContext)
        {
            _medicineService = medicineService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetById(id);

            if (medicine == null)
            {
                return NotFound();
            }
            else
            {
                MedicineInformation = medicine;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetById(id);
            if (medicine != null)
            {
                MedicineInformation = medicine;
                await _medicineService.Delete(medicine);
                await _hubContext.Clients.All.SendAsync("Delete_Medicine", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
