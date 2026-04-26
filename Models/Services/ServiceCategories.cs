namespace DHC_FSAP.Models.Services
{
    public class ServiceCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }


        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
