using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Repository.Data;

namespace TourismMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly TourismContext _context;
        public ReviewController(TourismContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var reviews = await _context.Reviews.Include(U => U.User).Include(P => P.Place).ToListAsync();

                return View(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var review = _context.Reviews.Include(U => U.User).Include(P => P.Place).FirstOrDefault(r => r.Id == id);

            if (review is null)
                return NotFound();


            return View(review);
        }


        //Get : open form of Delete
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review is null)
                return NotFound();

            return View(review);
        }

        //post : Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Review review)
        {
            try
            {
                var findWeview = _context.Reviews.Find(review.Id);

                if (review is null)
                    return NotFound();

                _context.Reviews.Remove(review);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(review);
            }
        }

    }
}
