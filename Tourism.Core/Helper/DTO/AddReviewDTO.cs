
using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class AddReviewDTO
    {
        public string? Message { get; set; }

        [Range(1, 5)]
        public float Rating { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }


    }
}
