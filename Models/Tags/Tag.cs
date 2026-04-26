using DHC_FSAP.Models.Services;
using DHC_FSAP.Models.Blogs;

namespace DHC_FSAP.Models.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;


        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
