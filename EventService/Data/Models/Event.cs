using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventService.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string OrganizerId { get; set; }  // FK to IdentityUser

        public IdentityUser Organizer { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public ICollection<EventCategory> EventCategories { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }


}
