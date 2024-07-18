using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.Helpers;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class CategoryController : BaseControllerMVC
    {
        private readonly IUnitOfWork<Category> _unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork<Category> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories;
            categories = await _unitOfWork.generic.GetAllAsync();

            if (categories is not null)
                return View(categories);
            else
                return BadRequest();

        }




        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var category = await _unitOfWork.generic.GetAsync(id.Value);
            var categorymapped = mapper.Map<Category, CategoryViewModel>(category);
            if (category is null)
                return NotFound();

            return View(viewname, categorymapped);
        }





        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel category)
        {
            category.ImgUrl = category.PhotoFile.FileName;
            if (category.PhotoFile != null)
            {
                category.ImgUrl = DocumentSetting.UploadFile(category.PhotoFile, "category");
            }
            var categorymapped = mapper.Map<CategoryViewModel, Category>(category);
            // categorymapped.ImgUrl= category.PhotoFile.FileName;
            var allcategory = await _unitOfWork.generic.GetAllAsync();
            bool exists = allcategory.Any(e => e.Name == categorymapped.Name);

            if (exists)
            {
                TempData["message"] = $"{categorymapped.Name} already exists in the database.";
                return View(category);
            }
            else
            {
                if (ModelState.IsValid)
                {

                    categorymapped.ImgUrl = $"images/category/{categorymapped.ImgUrl}";
                    _unitOfWork.generic.Add(categorymapped);
                    var count = _unitOfWork.Complet();

                    if (count > 0)
                        TempData["message"] = "Category Created succesfully";
                    else
                        TempData["message"] = "Category Failed Created";

                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CategoryViewModel category)
        {

            if (id != category.Id)
                return BadRequest();


            if (category.PhotoFile != null)
            {
                category.ImgUrl = category.PhotoFile.FileName;
                DocumentSetting.DeleteFile("category", category.ImgUrl);

                category.ImgUrl = DocumentSetting.UploadFile(category.PhotoFile, "category");
            }
            var categorymapped = mapper.Map<CategoryViewModel, Category>(category);

            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                    categorymapped.ImgUrl = $"images/category/{categorymapped.ImgUrl}";

                    _unitOfWork.generic.Update(categorymapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, CategoryViewModel category)
        {
            if (id != category.Id)
                return BadRequest();
            var categorymapped = mapper.Map<CategoryViewModel, Category>(category);
            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    _unitOfWork.generic.Delete(categorymapped);
                    var count = _unitOfWork.Complet();
                    if (count > 0)
                    {
                        DocumentSetting.DeleteFile("category", categorymapped.ImgUrl);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(category);
        }
    }
}
