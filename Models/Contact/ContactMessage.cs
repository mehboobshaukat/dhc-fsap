using DHC_FSAP.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHC_FSAP.Models.Contact
{
    public class ContactMessage
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
