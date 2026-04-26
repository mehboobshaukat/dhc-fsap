using DHC_FSAP.Models.Blogs;

namespace DHC_FSAP.DTOs.Blogs
{
    public class BlogCreateDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public BlogLanguage BlogLanguage { get; set; }
        public IFormFile? FeaturedImageURL { get; set; }
        
        public int BlogCategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
    }
}
