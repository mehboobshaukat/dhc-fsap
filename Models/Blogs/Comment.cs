using DHC_FSAP.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace DHC_FSAP.Models.Blogs
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsApproved { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
