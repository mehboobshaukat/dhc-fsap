namespace DHC_FSAP.DTOs.Services
{
    public class ServiceReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
    }
}
