using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Abouts;
using DHC_FSAP.Models.Abouts;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.About
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _dbContext;

        public AboutService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AboutDTO> GetAsync()
        {
            var about = await _dbContext.Abouts.FirstOrDefaultAsync();

            if (about == null)
            {
                return new AboutDTO();
            }

            return new AboutDTO
            {
                Mission = about.Mission,
                Vision = about.Vision,
                Background = about.Background,
                Goals = about.Goals,
            };
        }

        public async Task<bool> UpdateAsync(AboutDTO dto)
        {
            var about = await _dbContext.Abouts.FirstOrDefaultAsync();

            if(about == null)
            {
                // if not yet added about secion it's only for first time use
                about = new Models.Abouts.About
                {
                    Mission = dto.Mission,
                    Vision = dto.Vision,
                    Background = dto.Background,
                    Goals = dto.Goals,
                };

                _dbContext.Abouts.Add(about);
            }
            else
            {
                //update existing about section
                about.Mission = dto.Mission ?? about.Mission;
                about.Vision = dto.Vision ?? about.Vision;
                about.Background = dto.Background ?? about.Background;
                about.Goals = dto.Goals ?? about.Goals;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
