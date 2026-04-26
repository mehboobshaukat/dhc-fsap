namespace DHC_FSAP.DTOs.Blogs
{
    public class CommentReadDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
