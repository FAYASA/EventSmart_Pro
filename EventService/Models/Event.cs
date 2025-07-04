using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EventService.Models
{
    // Add the missing CreatedAt property to the Event class
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int VenueId { get; set; }

        [JsonIgnore]
        public Venue Venue { get; set; }
        public string OrganizerId { get; set; }

        [JsonIgnore]
        public ApplicationUser Organizer { get; set; }

        [JsonIgnore]
        public ICollection<Registration> Registrations { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        //public ICollection<EventCategory> EventCategories { get; set; }

        public DateTime CreatedAt { get; set; }
    }


}
