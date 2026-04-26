using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Tags;
using DHC_FSAP.Models.Tags;
using DHC_FSAP.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context) 
        {
            _context = context;
        }

        //get all tag
        public async Task<List<TagReadDTO>> GetAllAsync()
        {
            return await _context.Tags
                .OrderBy(t => t.Title)
                .Select(t => new TagReadDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Slug = t.Slug,
                })
                .ToListAsync();
        }

        //get by id
        public async Task<TagReadDTO?> GetByIdAsync(int id)
        {
            return await _context.Tags
                .Where(t => t.Id == id)
                .Select(t => new TagReadDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Slug = t.Slug,
                })
                .FirstOrDefaultAsync();
        }

        //create tag
        public async Task<bool> CreateAsync(TagCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (generatedSlug) => await _context.Tags.AnyAsync(x => x.Slug == generatedSlug)
             );

            var tag = new Tag
            {
                Title = dto.Title,
                Slug = slug,
            };

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        //update tag
        public async Task<bool> UpdateAsync(TagUpdateDTO dto)
        {
            var tag = await _context.Tags.FindAsync(dto.Id);
            if (tag == null) return false;

            if (tag.Title != dto.Title)
            {
                tag.Title = dto.Title;
                var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                    dto.Title,
                    async (generatedSlug) => await _context.Tags.AnyAsync(x => x.Slug == generatedSlug)
                );
            }

            await _context.SaveChangesAsync();
            return true;
        }

        //delete slug
        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
