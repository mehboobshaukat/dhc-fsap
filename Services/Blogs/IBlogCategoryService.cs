using DHC_FSAP.DTOs.Blogs;

namespace DHC_FSAP.Services.Blogs
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategoryReadDTO>> GetAllAsync();
        Task<BlogCategoryReadDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BlogCategoryCreateDTO dto);
        Task<bool> UpdateAsync(BlogCategoryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
