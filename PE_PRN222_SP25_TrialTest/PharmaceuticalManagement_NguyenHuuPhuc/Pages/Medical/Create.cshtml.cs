using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;
using System.Text.RegularExpressions;

namespace PsychologyHealthCare.RazorWebApp.Pages.AppointmentTrackings
{
    [Authorize(Roles = "2")]
    public class CreateModel : PageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly ManufacturesService _manufacturesService;
        public CreateModel(IMedicineService _edicineService, ManufacturesService manufacturesService)
        {
            _medicineService = _edicineService;
            _manufacturesService = manufacturesService;
        }

        public async Task<IActionResult> OnGet()
        {
            var dropList = await _manufacturesService.GetAllAsync();
            ViewData["ManufacturerName"] = new SelectList(dropList, "ManufacturerId", "ManufacturerName");
            return Page();
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsValidActiveIngredients(MedicineInformation.ActiveIngredients))
            {
                ModelState.AddModelError("MedicineInformation.ActiveIngredients",
                    "ActiveIngredients must be more than 10 characters, each word must start with a capital letter or a number, and cannot contain special characters like #, @, &, (, ).");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _medicineService.Create(MedicineInformation);

            return RedirectToPage("./Index");
        }

        private bool IsValidActiveIngredients(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length <= 10)
            {
                return false;
            }

            string pattern = @"^([A-Z0-9][a-z0-9]*)(\s[A-Z0-9][a-z0-9]*)*$";

            return Regex.IsMatch(input, pattern);
        }
    }
}
