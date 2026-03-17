using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using FManagement.Services.QuangND;
namespace FManagement.BlazorWebApp.QuangND.Components.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SystemAccountService _userAccountService;

        public LoginModel(SystemAccountService userAccountService) => _userAccountService = userAccountService;

        [BindProperty]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }


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
                    new Claim(ClaimTypes.NameIdentifier, userAccount.UserAccountId.ToString()),
                    new Claim(ClaimTypes.Role, userAccount.RoleId.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (userAccount.UserName != null)
                {
                    Response.Cookies.Append("UserName", userAccount.UserName);
                }

                //// After signing then redirect to default page
                var redirectUrl = string.IsNullOrEmpty(ReturnUrl) ? "/productionplanquangnds" : (ReturnUrl.StartsWith("/") ? ReturnUrl : "/" + ReturnUrl);
                return LocalRedirect(redirectUrl);
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                TempData["Message"] = "Login fail, please check your account";
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }
    }    
}
