using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Pages.Medical
{
    [Authorize(Roles = "2, 3")]
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
        public int TotalPages { get; set; }

        public IList<MedicineInformation> MedicineInformation { get; set; } = default!;

        private readonly IMedicineService _medicineService;

        public IndexModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> OnGetAsync(string active, string expire, string warn, int pageNumber = 1)
        {
            var items = string.IsNullOrEmpty(active) && string.IsNullOrEmpty(expire) && string.IsNullOrEmpty(warn)
                ? await _medicineService.GetAllAsync()
                : await _medicineService.Search(active, expire, warn);

            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double) PageSize);

            var pagedItems = items
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            TotalPages = totalPages;
            MedicineInformation = pagedItems;
            return Page();
        }
    }
}
