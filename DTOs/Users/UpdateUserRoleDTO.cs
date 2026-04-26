using DHC_FSAP.Models.Users;

namespace DHC_FSAP.DTOs.Users
{
    public class UpdateUserRoleDTO
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
