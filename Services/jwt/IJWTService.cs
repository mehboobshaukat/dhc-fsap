using DHC_FSAP.Models.Users;

namespace DHC_FSAP.Services.jwt
{
    public interface IJWTService
    {
        string GenerateToken(User user);
    }
}
