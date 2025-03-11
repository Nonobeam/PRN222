using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "2")]
    public class EditModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly ManufacturesService _manufacturesService;
        public EditModel(IMedicineService _edicineService, ManufacturesService manufacturesService)
        {
            _medicineService = _edicineService;
            _manufacturesService = manufacturesService;
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
            MedicineInformation = medicine;
            ViewData["ProgramId"] = new SelectList(await _manufacturesService.GetAllAsync(), "Id", "Name");
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

            await _medicineService.Update(MedicineInformation);
            return RedirectToPage("./Index");
        }
    }
}
