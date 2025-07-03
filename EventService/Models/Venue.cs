using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Event> Events { get; set; }
    }

}
