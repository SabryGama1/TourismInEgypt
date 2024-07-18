using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.Helpers;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class PlacePhotoController : Controller
    {
        private readonly IUnitOfWork<PlacePhotos> unitOfWork;

        private readonly IMapper mapper;


        public PlacePhotoController(IUnitOfWork<PlacePhotos> unitOfWork,
         IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.mapper = mapper;

        }

        // GET: CityPhotosController
        public async Task<IActionResult> Index()
        {
            IEnumerable<PlacePhotos> placePhotos;
            placePhotos = await unitOfWork.generic.GetAllAsync();


            var placePhviewModelList = mapper.Map<IEnumerable<PlacePhotos>, IEnumerable<PlacePhotoViewModel>>(placePhotos);

            if (placePhviewModelList is not null)
                return View(placePhviewModelList);
            else
                return BadRequest();

        }

        // GET: CityPhotosController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var placePh = await unitOfWork.generic.GetAsync(id.Value);


            if (placePh is null)
                return NotFound();

            var PlacePhmapped = mapper.Map<PlacePhotos, PlacePhotoViewModel>(placePh);

            return View(viewname, PlacePhmapped);
        }

        // GET: CityPhotosController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CityPhotosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlacePhotoViewModel PlacePhotos)
        {

            if (ModelState.IsValid)
            {

                var Placemapped = mapper.Map<PlacePhotoViewModel, PlacePhotos>(PlacePhotos);

                try
                {
                    Placemapped.Photo = $"images/places/{Placemapped.Photo}";
                    unitOfWork.generic.Add(Placemapped);
                    unitOfWork.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }


            }

            return View(PlacePhotos);

        }

        // GET: CityPhotosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            return await Details(id, "Edit");
        }

        // POST: CityPhotosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlacePhotoViewModel photosViewModel)
        {
            if (id != photosViewModel.Id)
                return BadRequest();

            if (photosViewModel.PhotoFile.FileName != null)
            {
                DocumentSetting.DeleteFile("places", photosViewModel.Photo);
                photosViewModel.Photo = DocumentSetting.UploadFile(photosViewModel.PhotoFile, "places");

            }


            if (ModelState.IsValid)  //server side validation
            {
                try
                {


                    var placephmapped = mapper.Map<PlacePhotoViewModel, PlacePhotos>(photosViewModel);
                    placephmapped.Photo = $"images/places/{placephmapped.Photo}";
                    unitOfWork.generic.Update(placephmapped);
                    var count = unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(photosViewModel);
        }

        // GET: CityPhotosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: CityPhotosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, PlacePhotoViewModel placePhotosViewModel)
        {
            if (id != placePhotosViewModel.Id)
                return BadRequest();

            try
            {
                var placemapped = mapper.Map<PlacePhotoViewModel, PlacePhotos>(placePhotosViewModel);

                unitOfWork.generic.Delete(placemapped);
                var count = unitOfWork.Complet();
                if (count > 0)
                    DocumentSetting.DeleteFile("places", placemapped.Photo);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                return View(placePhotosViewModel);
            }


        }
    }
}
