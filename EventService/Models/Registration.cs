using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventService.Models
{
    public class Registration
    {
        public int Id { get; set; }

        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public DateTime RegisteredAt { get; set; }
    }

}
