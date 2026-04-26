namespace DHC_FSAP.DTOs.Abouts
{
    public class TeamCreateDTO
    {
        public string Name { get; set; }
        public IFormFile? ImageURL { get; set; }
        public string Role { get; set; }
        public short SortOrder { get; set; }
    }
}
