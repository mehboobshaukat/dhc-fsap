using DHC_FSAP.Models.Bookings;

namespace DHC_FSAP.DTOs.Booking
{
    public class BookingReadDTO
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public int BookingCategory { get; set; }
        public string BookingCategoryName { get; set; }
        public int BookingType { get; set; }
        public string BookingTypeName { get; set; }
        public int BookingStatus { get; set; }
        public string BookingStatusName { get; set; }
        public string PreferredDateTime { get; set; }
        public DateOnly? FinalDate { get; set; }
        public TimeOnly? FinalTime { get; set; }
        public string? MeetingLink { get; set; }
        public string? Address { get; set; }
        public string? AdminMessage { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public int? ServiceId { get; set; }
        public string? ServiceTitle { get; set; }
    }
}
