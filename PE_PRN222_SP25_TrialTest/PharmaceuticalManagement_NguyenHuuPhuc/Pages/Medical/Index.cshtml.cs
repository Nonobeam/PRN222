using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Pages.Medical
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Active { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Expire { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Warn { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        public readonly int PageSize = 3;

        [BindProperty(SupportsGet = true)]
        public int TotalPages { get; set; }

        public IList<MedicineInformation> MedicineInformation { get; set; } = default!;

        private readonly IMedicineService _medicineService;

        public IndexModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> OnGetAsync(string? active, string? expire, string? warn, int pageNumber = 1)
        {
            List<MedicineInformation> totalItems = await _medicineService.GetAllAsync();
            TotalPages = (int)Math.Ceiling(totalItems.Count / (double)PageSize);
            MedicineInformation = await _medicineService.GetAllAsync(pageNumber);

            if (!string.IsNullOrEmpty(active) || !string.IsNullOrEmpty(expire) || !string.IsNullOrEmpty(warn))
            {
                if (User.IsInRole("3"))
                {
                    var totalSearchItems = await _medicineService.GetTotalCountAsync(active, expire, warn);
                    TotalPages = (int)Math.Ceiling(totalSearchItems / (double)PageSize);
                    MedicineInformation = await _medicineService.Search(active, expire, warn, pageNumber);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You do not have permission to search.");
                }
            }

            return Page();
        }
    }
}
