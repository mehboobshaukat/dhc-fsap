using DHC_FSAP.Models.Bookings;

namespace DHC_FSAP.DTOs.Booking
{
    public class BookingCreateDTO
    {
        public BookingCat BookingCategory { get; set; }
        public string Purpose { get; set; }
        public int? ServiceId { get; set; }
        public BookingType BookingType { get; set; }
        public string PreferredDateTime { get; set; }
    }
}
