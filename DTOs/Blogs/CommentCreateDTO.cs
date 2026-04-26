namespace DHC_FSAP.DTOs.Blogs
{
    public class CommentCreateDTO
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
