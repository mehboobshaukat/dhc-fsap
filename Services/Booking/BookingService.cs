using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Booking;
using DHC_FSAP.Models.Bookings;
using Microsoft.EntityFrameworkCore;
namespace DHC_FSAP.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        //create
        public async Task<bool> CreateAsync(int userId, BookingCreateDTO dto)
        {
            var booking = new Models.Bookings.Booking
            {
                BookingCategory = dto.BookingCategory,
                Purpose = dto.Purpose,
                ServiceId = dto.ServiceId,
                BookingType = dto.BookingType,
                PreferredDateTime = dto.PreferredDateTime,
                UserId = userId,
                BookingStatus = BookingStatus.Pending,
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        //get all
        public async Task<List<BookingReadDTO>> GetAllAsync()
        {
            return await _context.Bookings
                .Select(b => new BookingReadDTO
                {
                    Id = b.Id,
                    Purpose = b.Purpose,
                    BookingCategory = b.BookingCategory.GetHashCode(),
                    BookingCategoryName = b.BookingCategory.ToString(),
                    BookingStatus = b.BookingStatus.GetHashCode(),
                    BookingStatusName = b.BookingStatus.ToString(),
                    BookingType = b.BookingType.GetHashCode(),
                    BookingTypeName = b.BookingType.ToString(),
                    PreferredDateTime = b.PreferredDateTime,
                    FinalDate = b.FinalDate,
                    FinalTime = b.FinalTime,
                    MeetingLink = b.MeetingLink,
                    Address = b.Address,
                    AdminMessage = b.AdminMessage,

                    UserId = b.UserId,
                    UserName = b.User.Name,
                    UserEmail = b.User.Email,
                    UserPhone = b.User.Phone,

                    ServiceId = b.ServiceId,
                    ServiceTitle = b.Service != null? b.Service.Title : null
                })
                .ToListAsync();
        }

        // get by user booking
        public async Task<List<BookingReadDTO>> GetByUserAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new BookingReadDTO
                {
                    Id = b.Id,
                    Purpose = b.Purpose,
                    BookingCategory= b.BookingCategory.GetHashCode(),
                    BookingCategoryName = b.BookingCategory.ToString(),
                    BookingType= b.BookingType.GetHashCode(),
                    BookingTypeName = b.BookingType.ToString(),
                    BookingStatus= b.BookingStatus.GetHashCode(),
                    BookingStatusName = b.BookingStatus.ToString(),
                    PreferredDateTime= b.PreferredDateTime,
                    FinalDate = b.FinalDate,
                    FinalTime = b.FinalTime,
                    MeetingLink = b.MeetingLink,
                    Address = b.Address,
                    AdminMessage = b.AdminMessage,

                    ServiceId = b.ServiceId,
                    ServiceTitle = b.Service != null ? b.Service.Title : null
                })
                .ToListAsync();
        }

        //update
        public async Task<bool> UpdateAsync(int id, BookingUpdateDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            booking.BookingStatus = dto.BookingStatus;
            if (dto.FinalDate != null) booking.FinalDate = dto.FinalDate;
            if (dto.FinalTime != null) booking.FinalTime = dto.FinalTime;
            if (dto.MeetingLink != null) booking.MeetingLink = dto.MeetingLink;
            if (dto.Address != null) booking.Address = dto.Address;
            if (dto.AdminMessage != null) booking.AdminMessage = dto.AdminMessage;
            booking.HandledByAdmin = dto.HandledByAdmin;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //delet
        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
