using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class TripSearch
    {
        [Required]
        public string StartLocation { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
