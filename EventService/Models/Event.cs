using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventService.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public string OrganizerId { get; set; }
        public ApplicationUser Organizer { get; set; }

        public ICollection<Registration> Registrations { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }
    }


}
