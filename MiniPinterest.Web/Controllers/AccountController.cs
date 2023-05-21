using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models.ViewModels;

namespace MiniPinterest.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email
            };

            var registerResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (registerResult.Succeeded) 
            {
                var addRoleResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (addRoleResult.Succeeded) 
                {
                    // success
                    return RedirectToAction("Login");
                }
            }

            //error
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var model = new LoginRequest
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                loginRequest.Username, loginRequest.Password, false, false);

            if (signInResult != null  && signInResult.Succeeded) 
            {
                if (string.IsNullOrWhiteSpace(loginRequest.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginRequest.ReturnUrl);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
