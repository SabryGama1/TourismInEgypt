using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class PlaceController : BaseApiController
    {

        private readonly IPlaceRepository _placerepo;
        private readonly IMapper mapper;
        private readonly IReviewRepository _review;

        public PlaceController(IPlaceRepository placerepo, IMapper mapper, IReviewRepository review)
        {

            _placerepo = placerepo;
            this.mapper = mapper;
            _review = review;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDTO>>> GetAllPlaces(string? placeName)
        {

            var places = await _placerepo.GetAllAsync();
            if (!string.IsNullOrEmpty(placeName))
            {
                var results = places.Where(e => e.Name.ToLower().Contains(placeName.ToLower())).ToList();

                if (results.Count() == 0)
                    return NotFound("This Place Not Existing");
                else
                {
                    var placesearch = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceDTO>>(results);
                    return Ok(placesearch);

                }


            }


            var data = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceDTO>>(places);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDTO>> GetPlace(int id)
        {
            var place = await _placerepo.GetAsync(id);

            //var reviews =await GetAllReviewByPlaceId(id);

            if (place == null)
                return NotFound();


            var data = mapper.Map<Place, PlaceDTO>(place);

            //data.reviews = reviews;
            return Ok(data);


        }

        //      [HttpGet("[action]{PlaceId}")]
        //      public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllReviewByPlaceId(int PlaceId)
        //      {
        //          try
        //          {

        //		var review = await _review.GetAllReviewByPlaceIdAsync(PlaceId);

        //		if (review == null)
        //		{
        //			return NotFound("No Review on this Place");
        //		}
        //             var reviews=mapper.Map<IEnumerable<Review>,IEnumerable<ReviewDTO>>(review);
        //		return Ok(review);
        //	}
        //	catch
        //	{
        //		return NotFound("This Place Not Found");
        //	}
        //}

    }
}
