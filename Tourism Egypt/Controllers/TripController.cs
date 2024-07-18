using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class TripController : BaseApiController
    {
        private readonly IGenericRepository<Trip> _trip;
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;


        public TripController(IGenericRepository<Trip> trip
            , IMapper mapper, ITripRepository tripRepository)
        {
            _trip = trip;
            _mapper = mapper;
            _tripRepository = tripRepository;

        }


        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            var List = await _trip.GetAllAsync();
            if (List == null)
            {
                return BadRequest();
            }
            else
            {
                var data = _mapper.Map<IEnumerable<Trip>, IEnumerable<SimpleTripDto>>(List);
                return Ok(data);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TripDTO>> Details(int id)
        {
            var trip = await _trip.GetAsync(id);

            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                var places = await _tripRepository.GetplacesByIdofTrip(trip.Id);
                var data = _mapper.Map<Trip, TripDTO>(trip);
                //foreach (var item in places)
                //{
                //    var place = item.Place;
                //    var mapedPlace = _mapper.Map<Place , placeOfTripDto>(place);
                //    data.places.Add(mapedPlace);
                //}
                return Ok(data);
            }

        }


        [HttpGet("SearchTrip")]
        public async Task<IActionResult> SearchTrip([Required] string _city, DateTime? _startDate, DateTime? _endDate)
        {

            var Trips = await _trip.GetAllAsync();

            if (_startDate == null || _endDate == null)
            {
                var trps = Trips.Where(x => x.City.ToLower().Contains(_city.ToLower())).ToList();
                var tripMapper = _mapper.Map<List<Trip>, List<SimpleTripDto>>(trps);
                return Ok(tripMapper);
            }
            var trp = Trips.Where((x => x.City.ToLower().Contains(_city.ToLower()) && x.StartDate == _startDate &&
            x.EndDate == _endDate)).ToList();
            var tripMapper2 = _mapper.Map<IEnumerable<Trip>, IEnumerable<SimpleTripDto>>(trp);

            return Ok(tripMapper2);
        }
    }
}
