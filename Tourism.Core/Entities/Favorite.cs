namespace Tourism.Core.Entities
{
    public class Favorite : BaseEntity
    {

        public int UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
    }
}
