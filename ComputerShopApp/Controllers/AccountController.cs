using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
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
                return RedirectToAction("Index");
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
            if (result.Succeeded) return RedirectToAction("Index", "Account");
            else ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            return View(loginUserDTO);
        }
    }
}
