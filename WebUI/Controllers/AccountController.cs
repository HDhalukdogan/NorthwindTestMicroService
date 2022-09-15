using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000/identity/") };
                var response = await client.PostAsJsonAsync("auth/login", loginModel);
                var UserModel = response.Content.ReadFromJsonAsync<UserModel>().Result;
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, UserModel.Email));
                claims.Add(new Claim("token", UserModel.Token));
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
