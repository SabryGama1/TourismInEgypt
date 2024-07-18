using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IUnitOfWork<Place> _unitOfWork;
        private readonly IMapper mapper;

        public PlaceController(IUnitOfWork<Place> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;

        }

        // GET: CityController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Place> places;
            places = await _unitOfWork.generic.GetAllAsync();

            var placeviewModelList = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceViewModel>>(places);

            if (placeviewModelList is not null)
                return View(placeviewModelList);
            else
                return BadRequest();

        }

        // GET: CityController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var place = await _unitOfWork.generic.GetAsync(id.Value);

            var placemapped = mapper.Map<Place, PlaceViewModel>(place);

            if (placemapped is null)
                return NotFound();

            return View(viewname, placemapped);
        }

        // GET: CityController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlaceViewModel place)
        {

            var placemapped = mapper.Map<PlaceViewModel, Place>(place);

            if (ModelState.IsValid)
            {


                _unitOfWork.generic.Add(placemapped);
                var count = _unitOfWork.Complet();

                if (count > 0)
                    TempData["message"] = "place Created succesfully";
                else
                    TempData["message"] = "place Failed Created";

                return RedirectToAction("Index");
            }

            return View(place);
        }

        // GET: CityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, PlaceViewModel placeVM)
        {
            if (id != placeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                    var placemapped = mapper.Map<PlaceViewModel, Place>(placeVM);
                    _unitOfWork.generic.Update(placemapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(placeVM);
        }

        // GET: CityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute] int id, PlaceViewModel placeVM)
        {
            if (id != placeVM.Id)
                return BadRequest();


            var placemapped = mapper.Map<PlaceViewModel, Place>(placeVM);
            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    _unitOfWork.generic.Delete(placemapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(placeVM);
        }
    }
}
