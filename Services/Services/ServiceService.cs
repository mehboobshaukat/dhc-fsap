using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Services;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Services;
using DHC_FSAP.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;

        public ServiceService(AppDbContext context)
        {
            _context = context;
        }

        //get all
        public async Task<List<ServiceReadDTO>> GetAllAsync()
        {
            return await _context.Services
                .Include(s => s.ServiceCategory)
                .Include(s => s.Tags)
                .Select(s => new ServiceReadDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Slug = s.Slug,
                    Summary = s.Summary,
                    ImageURL = s.ImageURL,
                    Price = s.Price,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    CategoryName = s.ServiceCategory.Name,
                    Tags = s.Tags.Select(t => t.Title).ToList(),
                })
                .ToListAsync();
        }

        //get by id
        public async Task<ServiceReadDTO> GetByIdAsync(int id)
        {
            var s = await _context.Services
                .Include(s => s.ServiceCategory)
                .Include(s => s.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null) return null;

            return new ServiceReadDTO
            {
                Id = s.Id,
                Title = s.Title,
                Slug = s.Slug,
                Summary = s.Summary,
                Description = s.Description,
                ImageURL = s.ImageURL,
                Price = s.Price,
                IsActive = s.IsActive,
                CategoryName = s.ServiceCategory.Name,
                Tags = s.Tags.Select(t => t.Title).ToList()
            };
        }

        //get by id
        public async Task<ServiceReadDTO> GetBySlugAsync(string slug)
        {
            var s = await _context.Services
                .Include(s => s.ServiceCategory)
                .Include(s => s.Tags)
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if (s == null) return null;

            return new ServiceReadDTO
            {
                Id = s.Id,
                Title = s.Title,
                Slug = s.Slug,
                Summary = s.Summary,
                Description = s.Description,
                ImageURL = s.ImageURL,
                Price = s.Price,
                IsActive = s.IsActive,
                CategoryName = s.ServiceCategory.Name,
                Tags = s.Tags.Select(t => t.Title).ToList()
            };
        }

        //create 
        public async Task<bool> CreateAsync(ServiceCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (s) => await _context.Services.AnyAsync(x => x.Slug == s)
            );

            string imagePath = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);

            var tags = new List<Tag>();
            if(dto.TagIds != null)
            {
                tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.Id))
                    .ToListAsync();
            }

            var service = new Service
            {
                Title = dto.Title,
                Slug = slug,
                Summary = dto.summary,
                Description = dto.Description,
                ImageURL = imagePath,
                Price = dto.Price,
                IsActive = dto.IsActive,
                ServiceCategoryId = dto.ServiceCategoryId,
                Tags = tags
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return true;
        }

        //update
        public async Task<bool> UpdateAsync(ServiceUpdateDTO dto)
        {
            var service = await _context.Services
                .Include(s => s.Tags)
                .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (service == null) return false;

            if (dto.Title != null)
            {
                service.Title = dto.Title;

                service.Slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                    dto.Title,
                    async (s) => await _context.Services.AnyAsync(x => x.Slug == s && x.Id != dto.Id)
                );
            }

            if (dto.Summary != null) service.Summary = dto.Summary;
            if (dto.Description != null) service.Description = dto.Description;
            if (dto.Price.HasValue) service.Price = dto.Price.Value;
            if (dto.IsActive.HasValue) service.IsActive = dto.IsActive.Value;
            if (dto.ServiceCategoryId.HasValue) service.ServiceCategoryId = dto.ServiceCategoryId.Value;

            if (dto.ImageURL != null)
            {
                service.ImageURL = await HelperFunctionsClass.SaveImageAsync(dto.ImageURL);
            }

            if (dto.TagIds != null)
            {
                var tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.Id))
                    .ToListAsync();

                service.Tags = tags;
            }

            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //delete
        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
