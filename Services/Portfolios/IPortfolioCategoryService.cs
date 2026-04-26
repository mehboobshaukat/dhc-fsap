using DHC_FSAP.DTOs.Portfolios;

namespace DHC_FSAP.Services.Portfolios
{
    public interface IPortfolioCategoryService
    {
        Task<List<PortfolioCategoryListDTO>> GetAllAsync();
        Task<PortfolioCategoryListDTO?> GetByIdAsync(int id);

        Task<bool> CreateAsync(PortfolioCategoryCreateDTO dto);
        Task<bool> UpdateAsync(PortfolioCategoryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
