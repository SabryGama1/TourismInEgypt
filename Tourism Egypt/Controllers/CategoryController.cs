using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {

            _categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetailsDTO>>> GetAllCategories(string? categoryName)
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(categoryName))
            {
                var results = categories.Where(e => e.Name.ToLower().Contains(categoryName.ToLower())).ToList();

                if (results.Count() == 0)
                    return NotFound("This Category Not Existing");
                else
                {
                    var placesearch = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDetailsDTO>>(results);
                    return Ok(placesearch);
                }

            }
            var data = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDetailsDTO>>(categories);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategorySpecificWithPlaceDTO>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
                return NotFound("This Category Not Existing");

            // var places = await _categoryRepository.GetAllPlacesBySpecificCategory(id);
            //var placemapped =mapper.Map<ICollection<Place>,ICollection<PlaceDTO>>(places);
            var data = mapper.Map<Category, CategorySpecificWithPlaceDTO>(category);
            //data.Places = placemapped;


            return Ok(data);
        }
    }
}
