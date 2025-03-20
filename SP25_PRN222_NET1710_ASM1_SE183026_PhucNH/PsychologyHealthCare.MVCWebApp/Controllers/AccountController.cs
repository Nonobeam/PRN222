using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PsychologyHealthCare.Service;
namespace PsychologyHealthCare.MVCWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserAccountService _userAccountService;

        public AccountController(UserAccountService systemUserAccountService) => _userAccountService = systemUserAccountService;

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                var userAccount = await _userAccountService.Authenticate(userName, password);

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

                    return RedirectToAction("Index", "AppointmentTracking");
                } else
                {
                    ModelState.AddModelError("", "You do not have permissions to do this function!");
                    return View();
                }
            }
            catch (Exception ex)
            {

            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ModelState.AddModelError("", "Login failure");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Role");
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Forbidden()
        {
            return View();
        }
    }
}