using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Models.Blogs;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Blogs
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        //create comment
        public async Task<bool> CreateAsync(CommentCreateDTO dto)
        {
            var comment = new Comment
            {
                BlogId = dto.BlogId,
                UserId = dto.UserId,
                Content = dto.Content,
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        //get comment by blog
        public async Task<List<CommentReadDTO>> GetByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId && c.IsApproved && !c.IsDeleted)
                .Select(c => new CommentReadDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserName = c.User.UserName,
                    CreatedAt = c.CreatedAt,
                })
                .ToListAsync();
        }

        //get all
        public async Task<List<CommentAdminDTO>> GetAllAsync()
        {
            return await _context.Comments
                .Select(c => new CommentAdminDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserName = c.User.UserName,
                    CreatedAt = c.CreatedAt,
                    IsApproved = c.IsApproved,
                    IsDeleted = c.IsDeleted,
                })
                .ToListAsync();
        }

        //approved comment
        public async Task<bool> ApproveAsync(int id, bool status)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            comment.IsApproved = status;
            await _context.SaveChangesAsync();
            return true;
        }

        //soft delete
        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            comment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
