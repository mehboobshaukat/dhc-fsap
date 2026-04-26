using DHC_FSAP.Models.Tags;
using System.ComponentModel.DataAnnotations;

namespace DHC_FSAP.Models.Blogs
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public BlogLanguage BlogLanguage { get; set; } = BlogLanguage.English;
        public string Content { get; set; }
        public string? FeaturedImageURL { get; set; }
        public bool IsPublished { get; set; } = true;
        public bool IsPin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
