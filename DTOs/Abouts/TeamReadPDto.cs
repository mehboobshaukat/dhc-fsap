namespace DHC_FSAP.DTOs.Abouts
{
    public class TeamReadPDto
    {
        public string Name { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string Role { get; set; }
        public short SortOrder { get; set; }
    }
}
