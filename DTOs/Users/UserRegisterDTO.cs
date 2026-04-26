namespace DHC_FSAP.DTOs.Users
{
    public class UserRegisterDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile? ImageURL { get; set; }
        public string Password { get; set; }
    }
}
