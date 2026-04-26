namespace DHC_FSAP.Models.Abouts
{
    public class Team
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; } = string.Empty;
        public string Role { get; set; }
        public short SortOrder { get; set; }
    }
}
