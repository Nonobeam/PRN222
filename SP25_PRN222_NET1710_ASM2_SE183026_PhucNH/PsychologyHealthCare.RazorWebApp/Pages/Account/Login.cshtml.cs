using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsychologyHealthCare.Repository.Models;
using PsychologyHealthCare.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using PsychologyHealthCare.RazorWebApp.Models;

namespace PsychologyHeathCare.RazorWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
		private readonly UserAccountService _userAccountService;


        [BindProperty]
        public UserAccount UserAccount { get; set; }
		[BindProperty]
		public PsychologyHealthCare.RazorWebApp.Models.LoginRequest LoginRequest { get; set; }

        public LoginModel(UserAccountService userAccountService)
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
				if (userAccount != null && (userAccount.RoleId == 2 || userAccount.RoleId == 3))
				{
					var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, userAccount.UserName),
							new Claim(ClaimTypes.Role, userAccount.RoleId.ToString())
						};

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

					Response.Cookies.Append("UserName", userAccount.FullName);
					Response.Cookies.Append("Role", userAccount.RoleId.ToString());
					return RedirectToPage("/AppointmentTrackings/Index");
				} 
				else
				{
                    ModelState.AddModelError("", "You do not have permissions to do this function!");
                    return Page();
                }
			}
			catch (Exception)
			{

			}

			ModelState.AddModelError("", "Login failure");
			return Page();
		}

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			Response.Cookies.Delete("UserName");
			Response.Cookies.Delete("Role");
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}
	}
}
