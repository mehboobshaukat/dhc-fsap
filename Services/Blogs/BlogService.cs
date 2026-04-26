using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Abouts;
using DHC_FSAP.Models.Blogs;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context) 
        {
            _context = context;
        }

        //get all pin

        public async Task<List<BlogListDTO>> GetAllPinAsync()
        {
            return await _context.Blogs
                .Where(b => b.IsPin && b.IsPublished)
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BlogListDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Slug = b.Slug,
                    Summary = b.Summary,
                    FeaturedImageURL = b.FeaturedImageURL,
                    IsPin = b.IsPin,
                    CategoryName = b.BlogCategory.Title,
                    CreatedAt = b.CreatedAt
                    
                })
                .ToListAsync();
        }

        //get all
        public async Task<List<BlogListDTO>> GetAllAsync()
        {
            return await _context.Blogs
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BlogListDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Slug = b.Slug,
                    Summary = b.Summary,
                    FeaturedImageURL = b.FeaturedImageURL,
                    IsPin = b.IsPin,
                    CategoryName = b.BlogCategory.Title,
                    CreatedAt = b.CreatedAt
                })
                .ToListAsync();
        }

        //Get by slug
        public async Task<BlogDetailDTO?> GetBySlugAsync(string slug)
        {
            return await _context.Blogs
                .Where(b => b.Slug == slug && b.IsPublished)
                .Select(b => new BlogDetailDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Slug = b.Slug,
                    Summary = b.Summary,
                    Content = b.Content,
                    BlogLanguage = b.BlogLanguage,
                    FeaturedImageURL = b.FeaturedImageURL,
                    CreatedAt = b.CreatedAt,
                    CategoryName = b.BlogCategory.Title,
                    Tags = b.Tags.Select(t => t.Title).ToList()
                })
                .FirstOrDefaultAsync();
        }

        //create
        public async Task<bool> CreateAsync(BlogCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (s) =>  await _context.Blogs.AnyAsync(x => x.Slug == s)
            );

            var tags = await _context.Tags
                .Where(t => dto.TagIds.Contains(t.Id))
                .ToListAsync();

            var blog = new Blog
            {
                Title = dto.Title,
                Slug = slug,
                Summary = dto.Summary,
                Content = dto.Content,
                BlogLanguage = dto.BlogLanguage,
                BlogCategoryId = dto.BlogCategoryId,
                Tags = tags,
                CreatedAt = DateTime.UtcNow,
            };

            if (dto.FeaturedImageURL != null)
            {
                blog.FeaturedImageURL = await HelperFunctionsClass.SaveImageAsync(dto.FeaturedImageURL);
            }

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        //udate
        public async Task<bool> UpdateAsync(BlogUpdateDTO dto)
        {
            var blog = await _context.Blogs
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == dto.Id);

            if (blog == null) return false;

            if(blog.Title != null)
            {
                blog.Title = dto.Title;
                blog.Slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                    dto.Title,
                    async (s) => await _context.Blogs.AnyAsync(x => x.Slug == s && x.Id != dto.Id)
                );
            }

            if (!string.IsNullOrWhiteSpace(dto.Summary)) blog.Summary = dto.Summary;
            if (!string.IsNullOrWhiteSpace(dto.Content)) blog.Content = dto.Content;
            if (dto.FeaturedImageURL != null) blog.FeaturedImageURL = await HelperFunctionsClass.SaveImageAsync(dto.FeaturedImageURL);
            blog.BlogLanguage = dto.BlogLanguage;
            if (dto.IsPublished.HasValue) blog.IsPublished = dto.IsPublished.Value;
            if (dto.IsPin.HasValue) blog.IsPin = dto.IsPin.Value;
            blog.BlogCategoryId = dto.BlogCategoryId;
            blog.UpdatedAt = DateTime.UtcNow;


            if (dto.TagIds != null && dto.TagIds.Any())
            {
                var tags = await _context.Tags
                .Where(t => dto.TagIds.Contains(t.Id))
                .ToListAsync();

                blog.Tags = tags;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        //delete
        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
