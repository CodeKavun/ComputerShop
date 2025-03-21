using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComputerShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public AccountController(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid) return View(registerUserDTO);

            ShopUser shopUser = new ShopUser
            {
                UserName = registerUserDTO.Username,
                Email = registerUserDTO.Email,
                BirthDate = registerUserDTO.BirthDate
            };

            var result = await userManager.CreateAsync(shopUser, registerUserDTO.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(shopUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerUserDTO);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid) return View(loginUserDTO);

            ShopUser? shopUser = await userManager.FindByNameAsync(loginUserDTO.Username);
            if (shopUser == null)
            {
                ModelState.AddModelError(string.Empty, "User was not found");
                return View(loginUserDTO);
            }

            var result = await signInManager.PasswordSignInAsync(shopUser, loginUserDTO.Password, loginUserDTO.RememberMe, false);
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            else ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            return View(loginUserDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string? returnUrl = Url.Action("GoogleResponse", "Account");
            var authProperties = signInManager.ConfigureExternalAuthenticationProperties("Google", returnUrl);

            return Challenge(authProperties, "Google");
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            var loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null) return RedirectToAction("Index");

            var result = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, false);

            if (!result.Succeeded)
            {
                string[] userInfo = [
                    loginInfo.Principal.FindFirst(ClaimTypes.Surname)!.Value,
                    loginInfo.Principal.FindFirst(ClaimTypes.Email)!.Value
                    ];

                ShopUser? shopUser = await userManager.FindByEmailAsync(userInfo[1]);
                if (shopUser == null)
                {
                    shopUser = new ShopUser
                    {
                        Email = userInfo[1],
                        UserName = userInfo[1]
                    };

                    var createResult = await userManager.CreateAsync(shopUser);
                }

                var loginResult = await userManager.AddLoginAsync(shopUser, loginInfo);
                await signInManager.SignInAsync(shopUser, false);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
