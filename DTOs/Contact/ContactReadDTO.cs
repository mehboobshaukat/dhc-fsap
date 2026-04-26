namespace DHC_FSAP.DTOs.Contact
{
    public class ContactReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead  { get; set; }
    }
}
