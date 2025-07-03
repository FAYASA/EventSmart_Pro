using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }
    }


}
