using DHC_FSAP.Models.Services;
using DHC_FSAP.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace DHC_FSAP.Models.Bookings
{
    public class Booking
    {
        public int Id {  get; set; }
        public BookingCat BookingCategory { get; set; }
        public string Purpose { get; set; }
        public int UserId { get; set; }
        public int? ServiceId { get; set; }
        public BookingType BookingType { get; set; }
        public string PreferredDateTime { get; set; }
        public BookingStatus BookingStatus { get; set; }                                                                                                                                         
        public DateOnly? FinalDate { get; set; }
        public TimeOnly? FinalTime { get; set; }
        public string? Address { get; set; }
        public string? MeetingLink { get; set; }
        public string? AdminMessage { get; set; }
        public int? ForMeetingAdmin { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? HandledByAdmin {  get; set; }

        public User User { get; set; } 
        public Service? Service { get; set; }

    }
}
