namespace DHC_FSAP.DTOs.Users
{
    public class UpdateProfileDTO
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public IFormFile? ImageURL { get; set; }
    }
}
