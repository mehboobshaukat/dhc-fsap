namespace DHC_FSAP.DTOs.Portfolios
{
    public class PortfolioCreateDTO
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public IFormFile FeaturedImage { get; set; }
        public List<IFormFile>? GalleryImages { get; set; }

        public string? Technologies { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public string? Challenges { get; set; }
        public string? Solution { get; set; }

        public bool IsFeatured { get; set; }
        public bool IsPublished { get; set; }

        public int PortfolioCategoryId { get; set; }
    }
}
