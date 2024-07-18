using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            this.mapper = mapper;
        }
        // GET: RoleController
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);

        }

        // GET: RoleController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null)
                return NotFound();

            var mappedrole = mapper.Map<ApplicationRole, RoleViewModel>(role);
            return View(viewname, mappedrole);
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleViewModel.RoleName);
                try
                {
                    if (!roleExist)
                    {
                        var mappedrole = mapper.Map<RoleViewModel, ApplicationRole>(roleViewModel);
                        await _roleManager.CreateAsync(mappedrole);

                    }
                    else
                    {
                        ModelState.AddModelError("Name", "Role Name Is Exist");
                        return View(roleViewModel);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id.Value.ToString());

            if (role is null)
                return NotFound();

            var mappedrole = new RoleEditViewModel()
            {
                RoleName = role.Name,
            };
            return View(mappedrole);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, RoleEditViewModel roleEditView)
        {
            if (id != roleEditView.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleEditView.RoleName);

                try
                {
                    if (!roleExist)
                    {
                        var role = await _roleManager.FindByIdAsync(id.ToString());
                        role.Name = roleEditView.RoleName;
                        await _roleManager.UpdateAsync(role);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("Name", "Role Name Is Exist");
                        return View(roleEditView);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(roleEditView);
        }

        // GET: RoleController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");

            }




        }
    }
}
