using AutoMapper;
using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Users;
using ComputerShopApp.Models.ViewModels.Claims;
using ComputerShopApp.Models.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComputerShopApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<ShopUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ShopUser> shopUsers = await userManager.Users.ToListAsync();
            IEnumerable<ShopUserDTO> shopUserDTOs = mapper.Map<IEnumerable<ShopUserDTO>>(shopUsers);

            return View(shopUserDTOs);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound("User was not found");

            ShopUserDTO shopUserDTO = mapper.Map<ShopUserDTO>(shopUser);
            return View(shopUserDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShopUserDTO shopUserDTO)
        {
            if (!ModelState.IsValid) return View(shopUserDTO);

            ShopUser? shopUser = await userManager.FindByIdAsync(shopUserDTO.Id);
            if (shopUser == null)
            {
                ModelState.AddModelError(string.Empty, "User was not found");
                return View(shopUserDTO);
            }

            shopUser.Email = shopUserDTO.Email;
            shopUser.BirthDate = shopUserDTO.BirthDate;
            IdentityResult result = await userManager.UpdateAsync(shopUser);

            if (result.Succeeded) return RedirectToAction("Index");
            else
            {
                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
                return View(shopUser);
            }
        }

        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();

            ChangePasswordViewModel viewModel = new ChangePasswordViewModel
            {
                Id = shopUser.Id,
                Email = shopUser.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            ShopUser? shopUser = await userManager.FindByIdAsync(viewModel.Id);
            if (shopUser == null) return NotFound();

            IdentityResult result = await userManager.ChangePasswordAsync(shopUser, viewModel.OldPassword, viewModel.NewPassword);
            if (result.Succeeded) return RedirectToAction("Index");
            else
            {
                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
                return View(shopUser);
            }
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();

            ShopUserDTO shopUserDTO = mapper.Map<ShopUserDTO>(shopUser);
            return View(shopUserDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();

            await userManager.DeleteAsync(shopUser);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowClaims(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();

            IList<Claim> claims = await userManager.GetClaimsAsync(shopUser);
            IndexClaimsViewModel viewModel = new IndexClaimsViewModel
            {
                Claims = claims,
                UserName = shopUser.UserName!,
                Email = shopUser.Email!
            };

            return View("../Claims/Index", viewModel);
        }
    }
}
