using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Abouts;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Abouts;
using DHC_FSAP.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.About
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        //user side 
        public async Task<List<TeamReadPDto>> GetTeamByPublicAsync()
        {
            return await _context.Teams
                .OrderBy(t => t.SortOrder)
                .Select(t => new TeamReadPDto
                {
                    Name = t.Name,
                    ImageURL = t.ImageURL,
                    Role = t.Role,
                    SortOrder = t.SortOrder,
                })
                .ToListAsync();
        }

        //admin side
        public async Task<List<teamReadADTO>> GetTeamByAdminAsync()
        {
            return await _context.Teams
                .OrderBy(t => t.SortOrder)
                .Select(t => new teamReadADTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageURL = t.ImageURL,
                    Role = t.Role,
                    SortOrder = t.SortOrder,
                })
                .ToListAsync();
        }

        //get by id
        public async Task<teamReadADTO?> GetTeamByIdAsync(int id)
        {
            return await _context.Teams
                .Where(t => t.Id == id)
                .Select(t => new teamReadADTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageURL = t.ImageURL,
                    Role = t.Role,
                    SortOrder = t.SortOrder,
                })
                .FirstOrDefaultAsync();
        }

        //create team
        public async Task<bool> CreateTeamAsync(TeamCreateDTO dto)
        {
            var team = new Team
            {
                Name = dto.Name,
                Role = dto.Role,
                SortOrder = dto.SortOrder,
            };

            if (dto.ImageURL != null)
            {
                team.ImageURL = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);
            }

            _context.Teams.Add(team);

            await _context.SaveChangesAsync();
            return true;
        }

        //update team
        public async Task<bool> UpdateTeamAsync(TeamUpdateDTO dto)
        {
            var team = await _context.Teams.FindAsync(dto.Id);

            if (team == null) return false;

            if (dto.Name != null) team.Name = dto.Name;
            if (dto.ImageURL != null) team.ImageURL = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);
            if (dto.Role != null) team.Role = dto.Role;
            if (dto.SortOrder != null) team.SortOrder = dto.SortOrder;

            await _context.SaveChangesAsync();
            return true;
        }

        //delete
        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null) return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
