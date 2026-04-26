using DHC_FSAP.Models.Bookings;

namespace DHC_FSAP.DTOs.Booking
{
    public class BookingUpdateDTO
    {
        public BookingStatus BookingStatus { get; set; }
        public DateOnly? FinalDate { get; set; }
        public TimeOnly? FinalTime { get; set; }
        public string? MeetingLink { get; set; }
        public string? Address { get; set; }
        public string? AdminMessage { get; set; }
        public int? HandledByAdmin { get; set; }
    }
}
