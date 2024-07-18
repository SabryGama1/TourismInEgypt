using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class CityController : BaseControllerMVC
    {
        private readonly IUnitOfWork<City> _unitOfWork;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork<City> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: CityController
        public async Task<IActionResult> Index()
        {
            IEnumerable<City> cities;
            cities = await _unitOfWork.generic.GetAllAsync();

            if (cities is not null)
                return View(cities);
            else
                return BadRequest();

        }

        // GET: CityController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var city = await _unitOfWork.generic.GetAsync(id.Value);
            var citymapped = mapper.Map<City, CityViewModel>(city);

            if (citymapped is null)
                return NotFound();

            return View(viewname, citymapped);
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityViewModel city)
        {
            var citymapped = mapper.Map<CityViewModel, City>(city);
            var allcity = await _unitOfWork.generic.GetAllAsync();
            bool exists = allcity.Any(e => e.Name == citymapped.Name);

            if (exists)
            {
                TempData["message"] = $"{citymapped.Name} already exists in the database.";
                return View(city);
            }
            else
            {
                if (ModelState.IsValid)
                {

                    _unitOfWork.generic.Add(citymapped);
                    var count = _unitOfWork.Complet();

                    if (count > 0)
                        TempData["message"] = "City Created succesfully";
                    else
                        TempData["message"] = "City Failed Created";

                    return RedirectToAction("Index");
                }
            }

            return View(city);
        }

        // GET: CityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CityViewModel city)
        {
            if (id != city.Id)
                return BadRequest();
            var citymapped = mapper.Map<CityViewModel, City>(city);

            if (ModelState.IsValid)
            {
                _unitOfWork.generic.Update(citymapped);
                var count = _unitOfWork.Complet();

                if (count > 0)
                    TempData["message"] = "City Created succesfully";
                else
                    TempData["message"] = "City Failed Created";

                return RedirectToAction("Index");
            }


            return View(city);
        }

        // GET: CityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute] int id, CityViewModel city)
        {
            if (id != city.Id)
                return BadRequest();
            var citymapped = mapper.Map<CityViewModel, City>(city);
            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    _unitOfWork.generic.Delete(citymapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(city);
        }
    }
}
