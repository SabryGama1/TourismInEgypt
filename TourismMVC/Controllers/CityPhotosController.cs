using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.Helpers;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class CityPhotosController : BaseControllerMVC
    {
        private readonly IUnitOfWork<CityPhotos> unitOfWork;

        private readonly IMapper mapper;


        public CityPhotosController(IUnitOfWork<CityPhotos> unitOfWork,
         IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.mapper = mapper;

        }

        // GET: CityPhotosController
        public async Task<IActionResult> Index()
        {
            IEnumerable<CityPhotos> cityPhotos;
            cityPhotos = await unitOfWork.generic.GetAllAsync();


            var cityPhviewModelList = mapper.Map<IEnumerable<CityPhotos>, IEnumerable<CityPhotosViewModel>>(cityPhotos);

            if (cityPhviewModelList is not null)
                return View(cityPhviewModelList);
            else
                return BadRequest();

        }

        // GET: CityPhotosController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var cityPh = await unitOfWork.generic.GetAsync(id.Value);


            if (cityPh is null)
                return NotFound();



            var cityPhmapped = mapper.Map<CityPhotos, CityPhotosViewModel>(cityPh);
            return View(viewname, cityPhmapped);
        }

        // GET: CityPhotosController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CityPhotosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityPhotosViewModel cityPhotos)
        {

            if (ModelState.IsValid)
            {

                var Citymapped = mapper.Map<CityPhotosViewModel, CityPhotos>(cityPhotos);

                try
                {
                    Citymapped.Photo = $"images/cities/{Citymapped.Photo}";
                    unitOfWork.generic.Add(Citymapped);
                    unitOfWork.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }


            }

            return View(cityPhotos);

        }

        // GET: CityPhotosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            return await Details(id, "Edit");
        }

        // POST: CityPhotosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityPhotosViewModel photosViewModel)
        {

            //var city = unitOfWork.generic.GetAsync(id);
            //photosViewModel.PhotoFile.FileName = photosViewModel.Photo;

            if (id != photosViewModel.Id)
                return BadRequest();

            if (photosViewModel.PhotoFile.FileName != null)
            {
                DocumentSetting.DeleteFile("cities", photosViewModel.Photo);
                photosViewModel.Photo = DocumentSetting.UploadFile(photosViewModel.PhotoFile, "cities");

            }


            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    var cityphmapped = mapper.Map<CityPhotosViewModel, CityPhotos>(photosViewModel);
                    cityphmapped.Photo = $"images/cities/{cityphmapped.Photo}";

                    unitOfWork.generic.Update(cityphmapped);
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
        public IActionResult Delete([FromRoute] int? id, CityPhotosViewModel cityPhotosViewModel)
        {
            if (id != cityPhotosViewModel.Id)
                return BadRequest();

            try
            {
                var Citymapped = mapper.Map<CityPhotosViewModel, CityPhotos>(cityPhotosViewModel);

                unitOfWork.generic.Delete(Citymapped);
                var count = unitOfWork.Complet();
                if (count > 0)
                    DocumentSetting.DeleteFile("cities", Citymapped.Photo);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                return View(cityPhotosViewModel);
            }


        }
    }
}
