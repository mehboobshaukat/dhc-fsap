using DHC_FSAP.Data;
using DHC_FSAP.DTOs.Contact;
using DHC_FSAP.Models.Contact;
using Microsoft.EntityFrameworkCore;

namespace DHC_FSAP.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        //user send message to admin
        public async Task<bool> CreateAsync(ContactCreateDTO dto, int? userId)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                UserEmail = dto.UserEmail,
                Country = dto.Country,
                Subject = dto.Subject,
                Message = dto.Message,
                UserId = userId
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }

        //admin will see all messages
        public async Task<List<ContactReadDTO>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new ContactReadDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserEmail = c.UserEmail,
                    Country = c.Country,
                    Subject = c.Subject,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    IsRead = c.IsRead
                })
                .ToListAsync();
        }

        //get by user 
        public async Task<List<ContactReadDTO>> GetByUserAsync(string email)
        {
            return await _context.ContactMessages
                .Where(c => c.UserEmail == email)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new ContactReadDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserEmail = c.UserEmail,
                    Country = c.Country,
                    Subject = c.Subject,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    IsRead = c.IsRead
                })
                .ToListAsync();
        }

        //get by id
        public async Task<ContactReadDTO?> GetByIdAsync(int id)
        {
            return await _context.ContactMessages
                .Where(c => c.Id == id)
                .Select(c => new ContactReadDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserEmail = c.UserEmail,
                    Country = c.Country,
                    Subject = c.Subject,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    IsRead = c.IsRead
                })
                .FirstOrDefaultAsync();
        }

        //mark as read
        public async Task<bool> MarkAsReadAsync(int id)
        {
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg == null) return false; 

            msg.IsRead = true;
            await _context.SaveChangesAsync();
            return true;
        }

        //mark as unread
        public async Task<bool> MarkAsUnReadAsync(int id)
        {
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg == null) return false;

            msg.IsRead = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
