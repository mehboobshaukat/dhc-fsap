namespace DHC_FSAP.Models.Blogs
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
