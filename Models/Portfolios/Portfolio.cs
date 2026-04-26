namespace DHC_FSAP.Models.Portfolios
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public string FeaturedImageURL { get; set; }
        public string? GalleryImages { get; set; } // JSON

        public string? Technologies { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public string? Challenges { get; set; }
        public string? Solution { get; set; }

        public bool IsFeatured { get; set; } = false;
        public bool IsPublished { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int PortfolioCategoryId { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; }
    }
}
