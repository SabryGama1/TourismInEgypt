using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class Place_TripController : Controller
    {
        private readonly IUnitOfWork<Place_Trip> _placetripunit;
        private readonly IMapper mapper;
        private readonly IUnitOfWork<Place> _placeUnit;
        private readonly IUnitOfWork<Trip> _tripUnit;

        public Place_TripController(IUnitOfWork<Place_Trip> placetripunit, IMapper mapper, IUnitOfWork<Place> placeUnit, IUnitOfWork<Trip> tripUnit)
        {
            _placetripunit = placetripunit;
            this.mapper = mapper;
            _placeUnit = placeUnit;
            _tripUnit = tripUnit;
        }
        //Get: Show all place_trips 
        public async Task<IActionResult> Index()
        {
            var all = await _placetripunit.placeTrip.GetAllWithPlaceAndTrip();

            return View(all);
        }

        //Get : Get one 
        public async Task<IActionResult> Details(int id)
        {
            var one = await _placetripunit.placeTrip.GetOneWithPlaceAndTrip(id);

            if (one == null)
            {
                return NotFound();
            }
            else
                return View(one);
        }

        //Get : Open form of create
        public async Task<IActionResult> Create()
        {
            var PlaceList = await _placeUnit.generic.GetAllAsync();
            var TripList = await _tripUnit.generic.GetAllAsync();

            Place_TripModel tripModel = new Place_TripModel();

            tripModel.places = PlaceList;
            tripModel.trips = TripList;
            return View(tripModel);
        }

        //post : Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Place_TripModel model)
        {

            if (ModelState.IsValid)
            {
                var ptMapper = mapper.Map<Place_TripModel, Place_Trip>(model);

                try
                {

                    _placetripunit.generic.Add(ptMapper);
                    _placetripunit.Complet();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            var PlaceList = await _placeUnit.generic.GetAllAsync();
            var TripList = await _tripUnit.generic.GetAllAsync();

            Place_TripModel tripModel = new Place_TripModel();

            tripModel.places = PlaceList;
            tripModel.trips = TripList;

            return View(tripModel);

        }

        //Get : Open form of Edit

        public async Task<IActionResult> Edit(int id)
        {
            var edited = await _placetripunit.generic.GetAsync(id);
            if (edited == null)
            {

                return RedirectToAction("Index");
            }
            else
            {
                var PlaceList = await _placeUnit.generic.GetAllAsync();
                var TripList = await _tripUnit.generic.GetAllAsync();

                var PTmapper = mapper.Map<Place_Trip, Place_TripModel>(edited);
                PTmapper.places = PlaceList;
                PTmapper.trips = TripList;
                return View(PTmapper);
            }
        }

        //Post : Save Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Place_TripModel tripModel)
        {
            var PLModel = mapper.Map<Place_TripModel, Place_Trip>(tripModel);

            if (ModelState.IsValid)
            {
                try
                {
                    _placetripunit.generic.Update(PLModel);
                    _placetripunit.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            var PlaceList = await _placeUnit.generic.GetAllAsync();
            var TripList = await _tripUnit.generic.GetAllAsync();

            tripModel.places = PlaceList;
            tripModel.trips = TripList;
            return View(tripModel);

        }

        //Get : Open Form Of Delete
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _placetripunit.generic.GetAsync(id);

            if (deleted != null)
            {
                return View(deleted);
            }
            else
                return RedirectToAction("Index");
        }


        //Post : Save Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Place_Trip place_Trip)
        {
            if (place_Trip == null)
            {
                return BadRequest();
            }
            else
                try
                {
                    _placetripunit.generic.Delete(place_Trip);
                    _placetripunit.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(place_Trip);
                }
        }
    }
}
