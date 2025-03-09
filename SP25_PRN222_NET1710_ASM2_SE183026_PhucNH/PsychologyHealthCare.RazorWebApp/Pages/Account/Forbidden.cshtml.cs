#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.RazorWebApp.Models;

namespace PsychologyHeathCare.RazorWebApp.Pages.Account
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
