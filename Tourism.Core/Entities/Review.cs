using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Entities
{
    public class Review : BaseEntity
    {
        public string? Message { get; set; }

        [Range(1, 5)]
        public float Rating { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }


    }
}
