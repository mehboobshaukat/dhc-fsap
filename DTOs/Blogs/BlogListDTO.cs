namespace DHC_FSAP.DTOs.Blogs
{
    public class BlogListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string? FeaturedImageURL { get; set; }
        public bool IsPin { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
