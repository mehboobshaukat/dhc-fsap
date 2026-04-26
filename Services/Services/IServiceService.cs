using DHC_FSAP.DTOs.Services;

namespace DHC_FSAP.Services.Services
{
    public interface IServiceService
    {
        Task<List<ServiceReadDTO>> GetAllAsync();
        Task<ServiceReadDTO> GetByIdAsync(int id);
        Task<ServiceReadDTO> GetBySlugAsync(string slug);
        Task<bool> CreateAsync(ServiceCreateDTO dto);
        Task<bool> UpdateAsync(ServiceUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
