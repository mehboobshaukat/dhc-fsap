using Microsoft.AspNetCore.Routing.Constraints;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DHC_FSAP.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [MaxLength(15)]
        public string Phone { get; set; }
        public string? ImageURL { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public bool IsBlocked { get; set; } = false;
        public bool CanRequest { get; set; } = true;
        public bool EmailConfirmed { get; set; } = false;
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();
    }
}
