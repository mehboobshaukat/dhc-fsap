using DHC_FSAP.Models.Tags;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHC_FSAP.Models.Services
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


        public int ServiceCategoryId { get; set; }
        public ServiceCategories ServiceCategory { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
