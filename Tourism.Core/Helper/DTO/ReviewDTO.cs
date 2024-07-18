
using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        [Range(1, 5)]
        public float Rating { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }



    }
}
