using DHC_FSAP.DTOs.Tags;

namespace DHC_FSAP.Services.Tags
{
    public interface ITagService
    {
        Task<List<TagReadDTO>> GetAllAsync();
        Task<TagReadDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TagCreateDTO dto);
        Task<bool> UpdateAsync(TagUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
