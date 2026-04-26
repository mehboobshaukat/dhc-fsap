using DHC_FSAP.DTOs.Services;

namespace DHC_FSAP.Services.Services
{
    public interface IServiceCategoryService
    {
        Task<bool> CreateAsync(ServiceCategoryCreateDTO dto);
        Task<List<ServiceCategoryReadDTO>> GetAllAsync();
        Task<ServiceCategoryReadDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(ServiceCategoryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
