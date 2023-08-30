using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using products_test.ViewModels;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using products_test.Models;

namespace products_test.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthConfig _config;
        public AuthController(IOptions<AuthConfig> options)
        {
            _config = options.Value;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.Username == _config.User && model.Password == _config.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, model.Username)
                    };
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ProductsCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                    return RedirectToAction("Index", "Products");
                }
                ModelState.AddModelError("", "Неверные логин или пароль");
            }

            return View(model);
        }
    }
}
