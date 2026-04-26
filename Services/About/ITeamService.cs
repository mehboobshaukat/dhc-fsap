using DHC_FSAP.DTOs.Abouts;

namespace DHC_FSAP.Services.About
{
    public interface ITeamService
    {
        Task<List<TeamReadPDto>> GetTeamByPublicAsync();
        Task<List<teamReadADTO>> GetTeamByAdminAsync();
        Task<teamReadADTO?> GetTeamByIdAsync(int id);
        Task<bool> CreateTeamAsync(TeamCreateDTO dto);
        Task<bool> UpdateTeamAsync(TeamUpdateDTO dto);
        Task<bool> DeleteTeamAsync(int id);
    }
}
