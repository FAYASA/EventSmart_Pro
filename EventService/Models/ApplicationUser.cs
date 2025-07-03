using Microsoft.AspNetCore.Identity;

namespace EventService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Event> OrganizedEvents { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
