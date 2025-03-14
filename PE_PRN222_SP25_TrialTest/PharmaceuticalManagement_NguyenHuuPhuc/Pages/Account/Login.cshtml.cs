using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;
using PharmaceuticalManagement_NguyenHuuPhuc.Models;

namespace PharmaceuticalManagement_NguyenHuuPhuc.Pages.Account
{
    public class LoginModel : PageModel
    {
		private readonly StoreAccountService _userAccountService;

        [BindProperty]
        public StoreAccount UserAccount { get; set; }

		[BindProperty]
		public Models.LoginRequest LoginRequest { get; set; }

        public LoginModel(StoreAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

		public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
			try
			{
				var userAccount = await _userAccountService.Authenticate(LoginRequest.UserName, LoginRequest.Password);
				if (userAccount != null)
                {
                    if (userAccount.Role == 1)
					{
						ModelState.AddModelError("", "You do not have permission to do this function!");
                        return Page();
                    } 
					else
					{
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, userAccount.Role.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                        Response.Cookies.Append("Role", userAccount.Role.ToString());
                        return RedirectToPage("/Medical/Index");
                    }
				}
			}
			catch (Exception)
			{

			}

			ModelState.AddModelError("", "Login failure");
			return Page();
		}

		public async Task<IActionResult> OnGetLogout()
		{
			Response.Cookies.Delete("Role");
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}
	}
}
