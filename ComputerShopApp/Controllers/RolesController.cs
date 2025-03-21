using AutoMapper;
using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Roles;
using ComputerShopApp.Models.DTO.Users;
using ComputerShopApp.Models.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ShopUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<IdentityRole> roles = await roleManager.Roles.ToListAsync();
            IEnumerable<RoleDTO> roleDTOs = mapper.Map<IEnumerable<RoleDTO>>(roles);

            return View(roleDTOs);
        }

        [Authorize(Roles = "admin,manager")]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError(string.Empty, "Role name is not set!");
                return View(model: roleName);
            }

            IdentityRole role = new IdentityRole { Name = roleName };
            await roleManager.CreateAsync(role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList()
        {
            IEnumerable<ShopUser> shopUsers = await userManager.Users.ToListAsync();
            IEnumerable<ShopUserDTO> shopUserDTOs = mapper.Map<IEnumerable<ShopUserDTO>>(shopUsers);

            return View(shopUserDTOs);
        }

        public async Task<IActionResult> ChangeRoles(string? id)
        {
            if (id == null) return NotFound();

            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();

            List<IdentityRole> allRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(shopUser);
            ShopUserDTO shopUserDTO = mapper.Map<ShopUserDTO>(shopUser);

            ChangeRoleViewModel viewModel = new ChangeRoleViewModel
            {
                Id = shopUserDTO.Id,
                Email = shopUserDTO.Email,
                AllRoles = allRoles,
                UserRoles = userRoles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRoles(ChangeRoleViewModel viewModel)
        {
            ShopUser? shopUser = await userManager.FindByIdAsync(viewModel.Id);
            if (shopUser == null) return NotFound();

            var allRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(shopUser);

            if (ModelState.IsValid)
            {
                var addedRoles = viewModel.Roles.Except(userRoles);
                var deletedRoles = userRoles.Except(viewModel.Roles);

                await userManager.AddToRolesAsync(shopUser, addedRoles);
                await userManager.RemoveFromRolesAsync(shopUser, deletedRoles);
                return RedirectToAction("UserList");
            }

            viewModel.AllRoles = allRoles;
            viewModel.UserRoles = userRoles;
            viewModel.Email = shopUser.Email;

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();

            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            RoleDTO roleDTO = mapper.Map<RoleDTO>(role);
            return View(roleDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string? id)
        {
            if (id == null) return NotFound();

            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
