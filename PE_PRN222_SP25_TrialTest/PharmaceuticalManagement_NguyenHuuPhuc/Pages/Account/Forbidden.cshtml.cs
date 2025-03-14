#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmaceuticalManagement_NguyenHuuPhuc.Models;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Pages.Account
{
    public class ForbiddenModel : PageModel
    {

        [BindProperty]
        public ErrorViewModel Error { get; set; }
        public void OnGet()
        {
        }
    }
}
