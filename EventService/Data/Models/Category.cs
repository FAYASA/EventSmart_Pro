using System.ComponentModel.DataAnnotations;

namespace EventService.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<EventCategory> EventCategories { get; set; }
    }


}
