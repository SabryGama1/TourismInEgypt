
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class ReviewController : BaseApiController
    {
        public readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDTO review)
        {
            try
            {
                var mappedReview = _mapper.Map<AddReviewDTO, Review>(review);

                var addedReview = await _reviewRepository.AddReviewAsync(mappedReview);

                return Ok(new Response()
                {
                    Status = true,
                    Message = $"Added Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> UpdateReview(int id, ReviewDTO review)
        {
            try
            {

                var mappedReview = _mapper.Map<ReviewDTO, Review>(review);

                var updatedReview = await _reviewRepository.UpdateReviewAsync(id, mappedReview);
                return Ok(new Response()
                {
                    Status = true,
                    Message = $"Updated Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var existingReview = await _reviewRepository.GetReviewByIdAsync(id);

                if (existingReview == null)
                {
                    return NotFound("This Review No Existing");

                }
                await _reviewRepository.DeleteReviewAsync(id);
                return Ok(new Response()
                {
                    Status = true,
                    Message = $"Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewById(int id)
        {
            try
            {

                var review = await _reviewRepository.GetReviewByIdAsync(id);

                if (review == null)
                {
                    return NotFound("This Review No Existing");
                }
                return Ok(_mapper.Map<Review, ReviewDTO>(review));
            }
            catch
            {
                return NotFound("This Review Not Found");
            }
        }

        [HttpGet("[action]{PlaceId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllReviewByPlaceId(int PlaceId)
        {
            try
            {

                var reviews = await _reviewRepository.GetAllReviewByPlaceIdAsync(PlaceId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound("No Review on this Place");
                }

                return Ok(reviews);
            }
            catch
            {
                return NotFound("This Place Not Found");
            }
        }


    }
}
