using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventService.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public ICollection<Event> Events { get; set; }
    }

}
