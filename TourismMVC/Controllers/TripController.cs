using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using TourismMVC.Helpers;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class TripController : Controller
    {
        private readonly IUnitOfWork<Trip> _unitOfWork;
        private readonly IMapper _mapper;

        public TripController(IUnitOfWork<Trip> _unitOfWork, IMapper mapper)
        {
            this._unitOfWork = _unitOfWork;
            _mapper = mapper;
        }
        //Get Trip

        public async Task<IActionResult> Details(int id)
        {

            Trip trip = await _unitOfWork.generic.GetAsync(id);

            if (trip == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var tripMapper = _mapper.Map<Trip, TripViewModel>(trip);
                return View(tripMapper);
            }
        }


        //Get AllTrips
        public async Task<ActionResult<SimpleTripDto>> Index()
        {
            IEnumerable<Trip> trips;
            trips = await _unitOfWork.generic.GetAllAsync();
            return View(trips);
        }

        //Open view of create trip

        public IActionResult Create()
        {
            return View();
        }
        //post : save add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TripViewModel tripView)
        {

            tripView.ImgUrl = tripView.PhotoFile.FileName;

            if (tripView.PhotoFile != null)
            {
                tripView.ImgUrl = DocumentSetting.UploadFile(tripView.PhotoFile, "trips");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var tripMapper = _mapper.Map<TripViewModel, Trip>(tripView);
                    tripMapper.ImgUrl = $"images/trips/{tripMapper.ImgUrl}";
                    _unitOfWork.generic.Add(tripMapper);
                    _unitOfWork.Complet();


                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }

            return View("Create", tripView);
        }

        //Get : open form of edit
        public async Task<IActionResult> Edit(int id)
        {

            var trip = await _unitOfWork.generic.GetAsync(id);

            if (trip == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var tripModel = _mapper.Map<Trip, TripViewModel>(trip);
                return View(tripModel);
            }
        }

        //post : save edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TripViewModel tripView)
        {
            if (id != tripView.Id)
                return BadRequest();

            if (tripView.PhotoFile.FileName != null)
            {
                tripView.ImgUrl = tripView.PhotoFile.FileName;
                DocumentSetting.DeleteFile("trips", tripView.ImgUrl);
                tripView.ImgUrl = DocumentSetting.UploadFile(tripView.PhotoFile, "trips");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tripMapper = _mapper.Map<TripViewModel, Trip>(tripView);
                    tripMapper.ImgUrl = $"images/trips/{tripMapper.ImgUrl}";
                    _unitOfWork.generic.Update(tripMapper);
                    _unitOfWork.Complet();

                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }



            return View("Edit", tripView);
        }

        //Get : open form of delete

        public async Task<IActionResult> Delete(int id)
        {

            var trip = await _unitOfWork.generic.GetAsync(id);
            if (trip == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var tripMapper = _mapper.Map<Trip, TripViewModel>(trip);
                return View(tripMapper);
            }
        }

        // post : Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, TripViewModel tripView)
        {


            var tripMapper = _mapper.Map<TripViewModel, Trip>(tripView);
            try
            {
                _unitOfWork.generic.Delete(tripMapper);
                var count = _unitOfWork.Complet();
                if (count > 0)
                {
                    DocumentSetting.DeleteFile("trips", tripMapper.ImgUrl);
                }
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
            }

            return View(tripView);
        }
    }
}
