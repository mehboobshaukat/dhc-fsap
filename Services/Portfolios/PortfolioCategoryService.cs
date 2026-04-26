using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Portfolios;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Portfolios;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Portfolios
{
    public class PortfolioCategoryService : IPortfolioCategoryService
    {
        private readonly AppDbContext _context;

        public PortfolioCategoryService(AppDbContext context) 
        {
            _context = context;
        }


        public async Task<List<PortfolioCategoryListDTO>> GetAllAsync()
        {
            return await _context.PortfolioCategories
                .Select(c => new PortfolioCategoryListDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Slug = c.Slug
                })
                .ToListAsync();
        }

        public async Task<PortfolioCategoryListDTO?> GetByIdAsync(int id)
        {
            var category = await _context.PortfolioCategories.FindAsync(id);
            if (category == null) return null;

            return new PortfolioCategoryListDTO
            {
                Id = category.Id,
                Title = category.Title,
                Slug = category.Slug
            };
        }

        public async Task<bool> CreateAsync(PortfolioCategoryCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (s) => await _context.PortfolioCategories.AnyAsync(x => x.Slug == s)
            );

            var category = new PortfolioCategory
            {
                Title = dto.Title,
                Slug = slug
            };

            _context.PortfolioCategories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(PortfolioCategoryUpdateDTO dto)
        {
            var category = await _context.PortfolioCategories.FindAsync(dto.Id);
            if (category == null) return false;

            category.Title = dto.Title;

            category.Slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (s) => await _context.PortfolioCategories
                    .AnyAsync(x => x.Slug == s && x.Id != dto.Id)
            );

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.PortfolioCategories.FindAsync(id);
            if (category == null) return false;

            _context.PortfolioCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
