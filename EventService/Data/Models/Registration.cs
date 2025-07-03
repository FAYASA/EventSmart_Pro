using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventService.Data.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }  // FK to IdentityUser

        public IdentityUser User { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Pending"; // Optional: "Confirmed", "Cancelled"
    }

}
