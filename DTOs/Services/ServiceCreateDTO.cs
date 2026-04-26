namespace DHC_FSAP.DTOs.Services
{
    public class ServiceCreateDTO
    {
        public string Title { get; set; }
        public string summary { get; set; }
        public string Description { get; set; }
        public IFormFile ImageURL { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int ServiceCategoryId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
