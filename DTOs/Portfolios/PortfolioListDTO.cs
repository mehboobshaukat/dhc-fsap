namespace DHC_FSAP.DTOs.Portfolios
{
    public class PortfolioListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageURL { get; set; }
        public bool IsFeatured { get; set; }
        public string CategoryName { get; set; }
    }
}
