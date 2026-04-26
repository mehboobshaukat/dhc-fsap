using DHC_FSAP.DTOs.Booking;

namespace DHC_FSAP.Services.Booking
{
    public interface IBookingService
    {
        Task<bool> CreateAsync(int userId, BookingCreateDTO dto);
        Task<List<BookingReadDTO>> GetAllAsync();
        Task<List<BookingReadDTO>> GetByUserAsync(int userId);
        Task<bool> UpdateAsync(int id, BookingUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
