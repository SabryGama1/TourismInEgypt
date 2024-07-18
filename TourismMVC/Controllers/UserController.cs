using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                DisplayName = u.DisplayName,
                Email = u.Email,
                //PhoneNumber= u.PhoneNumber,
                Roles = _userManager.GetRolesAsync(u).Result

            }).ToListAsync();
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = await roleManager.Roles.ToListAsync();
            var viewmodel = new UserRoleViewModel()
            {
                Id = user.Id,
                Name = user.DisplayName,
                Roles = roles.Select(roles => new RoleEditViewModel()
                {
                    Id = roles.Id,
                    RoleName = roles.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, roles.Name).Result

                }).ToList(),

            };
            return View(viewmodel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoleViewModel userRoleView)
        {
            var user = await _userManager.FindByIdAsync(userRoleView.Id.ToString());
            var rolesuser = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoleView.Roles)
            {
                if (rolesuser.Any(r => r == role.RoleName) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                }
                if (!rolesuser.Any(r => r == role.RoleName) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);

                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ICollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
