using DHC_FSAP.DTOs.Portfolios;

namespace DHC_FSAP.Services.Portfolios
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListDTO>> GetAllAsync();
        Task<List<PortfolioListDTO>> GetFeaturedAsync();
        Task<PortfolioDetailDTO?> GetBySlugAsync(string slug);
        Task<PortfolioDetailDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PortfolioCreateDTO dto);
        Task<bool> UpdateAsync(PortfolioUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
