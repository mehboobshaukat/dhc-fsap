namespace DHC_FSAP.DTOs.Portfolios
{
    public class PortfolioDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        public string Description { get; set; }
        public string FeaturedImageURL { get; set; }
        public string? GalleryImages { get; set; }

        public string? Technologies { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GithubUrl { get; set; }

        public string? Challenges { get; set; }
        public string? Solution { get; set; }

        public string CategoryName { get; set; }
    }
}
