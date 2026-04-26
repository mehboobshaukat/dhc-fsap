using DHC_FSAP.DTOs.Blogs;

namespace DHC_FSAP.Services.Blogs
{
    public interface IBlogService
    {
        Task<List<BlogListDTO>> GetAllPinAsync();
        Task<List<BlogListDTO>> GetAllAsync();
        Task<BlogDetailDTO?> GetBySlugAsync(string slug);

        Task<bool> CreateAsync(BlogCreateDTO dto);
        Task<bool> UpdateAsync(BlogUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
