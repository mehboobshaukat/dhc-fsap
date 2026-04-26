using DHC_FSAP.DTOs.Contact;

namespace DHC_FSAP.Services.Contact
{
    public interface IContactService
    {
        Task<bool> CreateAsync(ContactCreateDTO dto, int? userId);
        Task<List<ContactReadDTO>> GetAllAsync();
        Task<ContactReadDTO> GetByIdAsync(int Id);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> MarkAsUnReadAsync(int id);
        Task<List<ContactReadDTO>> GetByUserAsync(string email);
    }
}
