using System.ComponentModel.DataAnnotations;

namespace EventService.Data.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        public ICollection<Event> Events { get; set; }
    }

}
