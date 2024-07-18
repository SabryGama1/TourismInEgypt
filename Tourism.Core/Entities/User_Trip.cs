namespace Tourism.Core.Entities
{
    public class User_Trip : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
