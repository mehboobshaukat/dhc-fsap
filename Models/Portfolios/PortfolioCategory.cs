using System.Collections.ObjectModel;

namespace DHC_FSAP.Models.Portfolios
{
    public class PortfolioCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}

