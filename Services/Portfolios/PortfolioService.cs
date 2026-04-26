using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Portfolios;
using DHC_FSAP.Helpers;
using DHC_FSAP.Models.Portfolios;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Portfolios
{
    public class PortfolioService : IPortfolioService
    {
        private readonly AppDbContext _context;

        public PortfolioService(AppDbContext context)
        {
            _context = context;
        }

        // Get all
        public async Task<List<PortfolioListDTO>> GetAllAsync()
        {
            return await _context.Portfolios
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PortfolioListDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    ShortDescription = p.ShortDescription,
                    FeaturedImageURL = p.FeaturedImageURL,
                    IsFeatured = p.IsFeatured,
                    CategoryName = p.PortfolioCategory.Title
                })
                .ToListAsync();
        }

        // Featured
        public async Task<List<PortfolioListDTO>> GetFeaturedAsync()
        {
            return await _context.Portfolios
                .Where(p => p.IsPublished && p.IsFeatured)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .Select(p => new PortfolioListDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    ShortDescription = p.ShortDescription,
                    FeaturedImageURL = p.FeaturedImageURL,
                    IsFeatured = p.IsFeatured,
                    CategoryName = p.PortfolioCategory.Title
                })
                .ToListAsync();
        }

        // Detail
        public async Task<PortfolioDetailDTO?> GetBySlugAsync(string slug)
        {
            return await _context.Portfolios
                .Where(p => p.Slug == slug && p.IsPublished)
                .Select(p => new PortfolioDetailDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    Description = p.Description,
                    FeaturedImageURL = p.FeaturedImageURL,
                    GalleryImages = p.GalleryImages,
                    Technologies = p.Technologies,
                    ProjectUrl = p.ProjectUrl,
                    GithubUrl = p.GithubUrl,
                    Challenges = p.Challenges,
                    Solution = p.Solution,
                    CategoryName = p.PortfolioCategory.Title
                })
                .FirstOrDefaultAsync();
        }


        public async Task<PortfolioDetailDTO?> GetByIdAsync(int id)
        {
            return await _context.Portfolios
                .Where(p => p.Id == id && p.IsPublished)
                .Select(p => new PortfolioDetailDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    Description = p.Description,
                    FeaturedImageURL = p.FeaturedImageURL,
                    GalleryImages = p.GalleryImages,
                    Technologies = p.Technologies,
                    ProjectUrl = p.ProjectUrl,
                    GithubUrl = p.GithubUrl,
                    Challenges = p.Challenges,
                    Solution = p.Solution,
                    CategoryName = p.PortfolioCategory.Title
                })
                .FirstOrDefaultAsync();
        }

        // Create
        public async Task<bool> CreateAsync(PortfolioCreateDTO dto)
        {
            var slug = await HelperFunctionsClass.GenerateUniqueSlugAsync(
                dto.Title,
                async (s) => await _context.Portfolios.AnyAsync(x => x.Slug == s)
            );

            var portfolio = new Portfolio
            {
                Title = dto.Title,
                Slug = slug,
                ShortDescription = dto.ShortDescription,
                Description = dto.Description,
                PortfolioCategoryId = dto.PortfolioCategoryId,
                Technologies = dto.Technologies,
                ProjectUrl = dto.ProjectUrl,
                GithubUrl = dto.GithubUrl,
                Challenges = dto.Challenges,
                Solution = dto.Solution,
                IsFeatured = dto.IsFeatured,
                IsPublished = dto.IsPublished,
                CreatedAt = DateTime.UtcNow
            };

            if (dto.GalleryImages != null && dto.GalleryImages.Any())
            {
                var imageUrls = new List<string>();

                foreach (var file in dto.GalleryImages)
                {
                    var url = await HelperFunctionsClass.SaveImageAsync(file);
                    imageUrls.Add(url);
                }

                portfolio.GalleryImages = System.Text.Json.JsonSerializer.Serialize(imageUrls);
            }

            if (dto.FeaturedImage != null)
            {
                portfolio.FeaturedImageURL = await HelperFunctionsClass.SaveImageAsync(dto.FeaturedImage);
            }

            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return true;
        }

        // Update
        public async Task<bool> UpdateAsync(PortfolioUpdateDTO dto)
        {
            var portfolio = await _context.Portfolios.FindAsync(dto.Id);
            if (portfolio == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Title))
                portfolio.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.ShortDescription))
                portfolio.ShortDescription = dto.ShortDescription;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                portfolio.Description = dto.Description;

            if (dto.PortfolioCategoryId > 0)
                portfolio.PortfolioCategoryId = dto.PortfolioCategoryId;

            if (dto.FeaturedImage != null)
                portfolio.FeaturedImageURL = await HelperFunctionsClass.SaveImageAsync(dto.FeaturedImage);

            if (dto.IsFeatured.HasValue)
                portfolio.IsFeatured = dto.IsFeatured.Value;

            if (dto.IsPublished.HasValue)
                portfolio.IsPublished = dto.IsPublished.Value;

            if (!string.IsNullOrWhiteSpace(dto.Technologies))
                portfolio.Technologies = dto.Technologies;

            if (!string.IsNullOrWhiteSpace(dto.ProjectUrl))
                portfolio.ProjectUrl = dto.ProjectUrl;

            if (!string.IsNullOrWhiteSpace(dto.GithubUrl))
                portfolio.GithubUrl = dto.GithubUrl;

            if (!string.IsNullOrWhiteSpace(dto.Challenges))
                portfolio.Challenges = dto.Challenges;

            if (!string.IsNullOrWhiteSpace(dto.Solution))
                portfolio.Solution = dto.Solution;

            if (dto.GalleryImages != null && dto.GalleryImages.Any())
            {
                var imageUrls = new List<string>();

                foreach (var file in dto.GalleryImages)
                {
                    var url = await HelperFunctionsClass.SaveImageAsync(file);
                    imageUrls.Add(url);
                }

                portfolio.GalleryImages = System.Text.Json.JsonSerializer.Serialize(imageUrls);
            }

            portfolio.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null) return false;

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
