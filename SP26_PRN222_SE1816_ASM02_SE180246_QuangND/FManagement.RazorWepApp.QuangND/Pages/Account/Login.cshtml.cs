using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using FManagement.Services.QuangND;
using Microsoft.AspNetCore.Authorization;

namespace FManagement.RazorWepApp.QuangND.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SystemAccountService _userAccountService;

        public LoginModel(SystemAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [BindProperty]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var userAccount = await _userAccountService.GetByUserAccountAsync(UserName, Password);

            if (userAccount != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, userAccount.RoleId.ToString()),
                    new Claim("UserId", userAccount.UserAccountId.ToString()) // Tracking User ID for LastModifiedBy
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                Response.Cookies.Append("UserName", UserName ?? string.Empty);

                // After signing then redirect to default page
                return RedirectToPage("/ProductionPlanQuangNds/Index");
            }
            else
            {
                TempData["Message"] = "Login fail, please check your account";
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }
    }
}
