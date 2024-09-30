using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonPointOfSaleFinal.App.Models;
using MonPointOfSaleFinal.Entities.ViewModels.Administraion;

namespace MonPointOfSaleFinal.App.Controllers
{
    public class AdministrationController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUser> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region Roles
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(r=> new RolesViewModel()
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToListAsync();           
            return View(roles);
        }
        public IActionResult NewRole()
        { 
            var role = new RolesViewModel() { Id = Guid.NewGuid().ToString() };
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> NewRole(RolesViewModel item)
        {
           
            if(ModelState.IsValid)
            {
                var role = new IdentityRole { Id = item.Id, Name = item.RoleName };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(item);
        }
        #endregion

        #region Users
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.Select(u=> new UserViewModel()
            {
                Id =u.Id,
                UserName = u.UserName,
                Email = u.Email,
                UserRoles = _userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(users);
        }
        
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("GetAllUsers");
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                UserRoles = roles.Select(r=> new SelectedRoles()
                {
                    RoleName = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return RedirectToAction("GetAllUsers");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.UserRoles.Where(r => r.IsSelected == true).Select(ur => ur.RoleName));
            return RedirectToAction("GetAllUsers");
        }
        #endregion
    }
}
