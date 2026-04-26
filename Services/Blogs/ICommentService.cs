using DHC_FSAP.DTOs.Blogs;

namespace DHC_FSAP.Services.Blogs
{
    public interface ICommentService
    {
        Task<bool> CreateAsync(CommentCreateDTO dto);
        Task<List<CommentReadDTO>> GetByBlogIdAsync(int blogId);
        Task<List<CommentAdminDTO>> GetAllAsync();

        Task<bool> ApproveAsync(int id, bool status);
        Task<bool> DeleteAsync(int id);
    }
}
