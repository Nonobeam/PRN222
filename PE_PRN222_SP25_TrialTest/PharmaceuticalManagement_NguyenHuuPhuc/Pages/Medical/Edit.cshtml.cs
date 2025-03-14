using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Pages.Medical
{
    [Authorize(Roles = "2")]
    public class EditModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly ManufacturesService _manufacturesService;
        public EditModel(IMedicineService medicineService, ManufacturesService manufacturesService)
        {
            _medicineService = medicineService;
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
            ViewData["ManufacturerId"] = new SelectList(await _manufacturesService.GetAllAsync(), "ManufacturerId", "ManufacturerName");
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
