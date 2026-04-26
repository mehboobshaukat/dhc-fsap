namespace DHC_FSAP.DTOs.Abouts
{
    public class TeamUpdateDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public IFormFile? ImageURL { get; set; }
        public string Role { get; set; }
        public short SortOrder { get; set; }
    }
}
