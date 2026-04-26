namespace DHC_FSAP.DTOs.Blogs
{
    public class CommentAdminDTO : CommentReadDTO
    {
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
