namespace DHC_FSAP.DTOs.Users
{
    public class AuthResponseDTO
    {
        public string Token {  get; set; }
        public UserResponseDTO User { get; set; }
    }
}
