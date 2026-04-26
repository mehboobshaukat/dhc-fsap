using DHC_FSAP.DTOs.Abouts;

namespace DHC_FSAP.Services.About
{
    public interface IAboutService
    {
        Task<AboutDTO> GetAsync();
        Task<bool> UpdateAsync(AboutDTO dto);
    }
}
