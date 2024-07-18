using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class SearchController : BaseApiController
    {
        private readonly TourismContext _context;

        public SearchController(TourismContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Find(string SearchByCityOrPlace)
        {
            if (!string.IsNullOrEmpty(SearchByCityOrPlace))
            {
                var items = _context.Places
                    .Include(b => b.City)
                    .Where(b => !b.IsDeleted &&
                        (
                            b.Name.Trim().ToLower().Contains(SearchByCityOrPlace.Trim().ToLower()) ||
                            b.Location.Trim().ToLower().Contains(SearchByCityOrPlace.Trim().ToLower()) ||
                            b.City.Name.Trim().ToLower().Contains(SearchByCityOrPlace.Trim().ToLower()) ||
                            b.City.Location.Trim().ToLower().Contains(SearchByCityOrPlace.Trim().ToLower())
                        ));

                if (items.Any())
                {
                    var result = items.Select(b => new
                    {
                        City = b.City.Name,
                        Name = b.Name,
                        Description = b.Description,
                        Location = b.Location,
                        Link = b.Link
                    }).ToList();

                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found for the search.");
                }
            }
            else
            {
                return BadRequest("Can't search with an empty string.");
            }
        }

    }
}
