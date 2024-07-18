using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class UserTripController : BaseApiController
    {
        private readonly IGenericRepository<User_Trip> _userTrip;
        public UserTripController(IGenericRepository<User_Trip> userTrip)
        {
            _userTrip = userTrip;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var List = await _userTrip.GetAllAsync();
            if (List == null)
            {
                return BadRequest();
            }
            return Ok(List);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var UT = await _userTrip.GetAsync(id);
            if (UT == null)
            {
                return NotFound();
            }
            return Ok(UT);
        }
    }
}
