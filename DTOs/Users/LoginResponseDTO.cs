namespace DHC_FSAP.DTOs.Users
{
    public class LoginResponseDTO
    {
        public UserResponseDTO User { get; set; }
        public string Token { get; set; }
    }
}
