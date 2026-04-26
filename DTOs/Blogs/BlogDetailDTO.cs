using DHC_FSAP.Models.Blogs;

namespace DHC_FSAP.DTOs.Blogs
{
    public class BlogDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public BlogLanguage BlogLanguage { get; set; }
        public string? FeaturedImageURL { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
    }
}
