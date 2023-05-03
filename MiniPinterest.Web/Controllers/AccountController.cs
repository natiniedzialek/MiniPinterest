using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models.ViewModels;

namespace MiniPinterest.Web.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> userManager;
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
                    return RedirectToAction("Register");
                }
            }

            //error
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                loginRequest.Username, loginRequest.Password, false, false);

            if (signInResult != null  && signInResult.Succeeded) 
            {
                return RedirectToAction("Index", "Home");
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
