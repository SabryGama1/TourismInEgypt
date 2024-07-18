using System.ComponentModel;
using Tourism.Core.Entities;
using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
    public class Place_TripModel
    {
        public int Id { get; set; }
        [UniquePlaceTrip]
        [DisplayName("Place Name")]
        public int PlaceId { get; set; }
        [DisplayName("Trip Name")]
        public int TripId { get; set; }

        public IEnumerable<Place> places { get; set; } = new List<Place>();
        public IEnumerable<Trip> trips { get; set; } = new List<Trip>();

    }
}
