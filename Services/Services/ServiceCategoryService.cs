using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Services;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly AppDbContext _context;

        public ServiceCategoryService(AppDbContext context)
        {
            _context = context;
        }

        //create
        public async Task<bool> CreateAsync(ServiceCategoryCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Name,
                async s => await _context.ServiceCategories.AnyAsync(c => c.Slug == s)
            );

            var category = new ServiceCategories
            {
                Name = dto.Name,
                Slug = slug,
            };

            await _context.ServiceCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //get all
        public async Task<List<ServiceCategoryReadDTO>> GetAllAsync()
        {
            return await _context.ServiceCategories
                .Select(c => new ServiceCategoryReadDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                })
                .ToListAsync();
        }

        //get by id
        public async Task<ServiceCategoryReadDTO?> GetByIdAsync(int id)
        {
            return await _context.ServiceCategories
                .Where(c => c.Id == id)
                .Select(c => new ServiceCategoryReadDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                })
                .FirstOrDefaultAsync();
        }

        //update
        public async Task<bool> UpdateAsync(ServiceCategoryUpdateDTO dto)
        {
            var category = await _context.ServiceCategories.FindAsync(dto.Id);
            if (category == null) return false;
            
            if(!string.IsNullOrWhiteSpace(dto.Name))
            {
                category.Name = dto.Name;

                category.Slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                    dto.Name,
                    async s => await _context.ServiceCategories.AnyAsync(c => c.Slug == s && c.Id != dto.Id)
                );
            }

            await _context.SaveChangesAsync();
            return true;
        }

        //delete
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.ServiceCategories.FindAsync(id);
            if (category == null) return false;

            _context.ServiceCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
