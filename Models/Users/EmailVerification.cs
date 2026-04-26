namespace DHC_FSAP.Models.Users
{
    public class EmailVerification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsUsed { get; set; }
        public DateTime? Expire => CreatedAt.AddHours(12);
        public User User { get; set; }
    }
}
