using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Blogs;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Blogs
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly AppDbContext _context;

        public BlogCategoryService(AppDbContext context)
        {
            _context = context;
        }

        //get all
        public async Task<List<BlogCategoryReadDTO>> GetAllAsync()
        {
            return await _context.BlogCategories
                .OrderBy(x => x.Title)
                .Select(x => new BlogCategoryReadDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug
                })
                .ToListAsync();
        }

        //get by id
        public async Task<BlogCategoryReadDTO> GetByIdAsync(int id)
        {
            return await _context.BlogCategories
                .Where(x => x.Id == id)
                .Select(x => new BlogCategoryReadDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug
                })
                .FirstOrDefaultAsync();
        }

        //create category
        public async Task<bool> CreateAsync(BlogCategoryCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (generatedSlug) => await _context.BlogCategories
                .AnyAsync(x => x.Slug == generatedSlug)
            );

            var category = new BlogCategory
            {
                Title = dto.Title,
                Slug = slug
            };

            _context.BlogCategories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        //update category
        public async Task<bool> UpdateAsync(BlogCategoryUpdateDTO dto)
        {
            var category = await _context.BlogCategories.FindAsync(dto.Id);
            if (category == null) return false;

            if (category.Title !=  dto.Title)
            {
                category.Title = dto.Title;
                category.Slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                        dto.Title,
                        async (generatedSlug) => await _context.BlogCategories
                        .AnyAsync(x => x.Slug == generatedSlug && x.Id != dto.Id));
            }

            await _context.SaveChangesAsync();
            return true;
        }

        //delete category
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.BlogCategories.FindAsync(id);
            if (category == null) return false;

            _context.BlogCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
