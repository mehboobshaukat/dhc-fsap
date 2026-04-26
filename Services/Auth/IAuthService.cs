using DHC_FSAP.DTOs.Users;

namespace DHC_FSAP.Services.Auth
{
    public interface IAuthService
    {
        Task<UserResponseDTO> RegisterAsync(UserRegisterDTO dto);
        Task<LoginResponseDTO> LoginAsync(UserLoginDTO dto);

        Task<UserResponseDTO> GetProfileAsync(int userId);
        Task<bool> ResendVerificationAsync(string email);
        Task<bool> VerifyEmailAsync(string token);
        Task<bool> UpdateProfileAsync(int userId, UpdateProfileDTO dto);

        Task<bool> ForgetPasswordAsync(ForgetPasswordDTO dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO dto);

        Task<List<UserListDTO>> GetAllUsersAsync();
        Task<UserListDTO> GetUserByIdAsync(int userId);

        Task<bool> BlockUserAsync(int userId);
        Task<bool> UnblockUserAsync(int userId);

        Task<bool> UpdateUserRoleAsync(UpdateUserRoleDTO dto);

        Task<bool> DeleteUserAsync(int userId);
    }
}
