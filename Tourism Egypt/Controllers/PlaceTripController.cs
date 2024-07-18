using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{

    public class PlaceTripController : BaseApiController
    {
        private readonly IGenericRepository<Place_Trip> _placetrip;
        private readonly IPlace_TripRepository _place_trip;
        public PlaceTripController(IGenericRepository<Place_Trip> placetrip, IPlace_TripRepository place_trip)
        {
            _placetrip = placetrip;
            _place_trip = place_trip;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var List = await _placetrip.GetAllAsync();
            if (List == null)
            {
                return BadRequest();

            }
            else
                return Ok(List);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var PT = await _placetrip.GetAsync(id);

            if (PT == null)
            {
                return NotFound();
            }
            else
                return Ok(PT);
        }


        //[HttpGet("TripDetails")]
        //public async Task<IActionResult> TripDetails(int id)
        //{
        //    var trip = await _place_trip.GetTripWithplaces(id);
        //    return Ok(trip);
        //}
    }
}
