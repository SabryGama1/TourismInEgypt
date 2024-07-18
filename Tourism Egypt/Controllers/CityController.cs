using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class CityController : BaseApiController
    {

        private readonly ICityRepository _cityrepo;
        private readonly IMapper mapper;

        public CityController(ICityRepository cityrepo, IMapper mapper)
        {
            _cityrepo = cityrepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetAllCities(string? cityName)
        {
            var cities = await _cityrepo.GetAllAsync();
            if (!string.IsNullOrEmpty(cityName))
            {
                var results = cities.Where(e => e.Name.ToLower().Contains(cityName.ToLower())).ToList();
                if (results.Count() == 0)
                    return NotFound("This City Not Existing");
                else
                {
                    var placesearch = mapper.Map<IEnumerable<City>, IEnumerable<CityDTO>>(results);
                    return Ok(placesearch);
                }

            }
            var data = mapper.Map<IEnumerable<City>, IEnumerable<CityDTO>>(cities);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _cityrepo.GetAsync(id);

            if (city == null)
                return NotFound();

            // var photos =await _cityrepo.GetAllPhotoBySpecIdAsync(id);
            var data = mapper.Map<City, CityDTO>(city);

            return Ok(data);
        }
    }
}
