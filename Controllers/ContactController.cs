using DHC_FSAP.DTOs.Contact;
using DHC_FSAP.Services.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        //user send message
        [HttpPost("send-message")]
        public async Task<IActionResult> Create(ContactCreateDTO dto)
        {
            // if user is login then we'll get userId through token
            int? userId = User.Identity.IsAuthenticated
                ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
                : null;

            var result = await _service.CreateAsync(dto, userId);
            return Ok(result);
        }

        //admin will get list of all messages
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // get by id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        //mark as read
        [HttpPut("mark-read/{id}")]
        public async Task<IActionResult> MarkRead(int id)
        {
            var result = await _service.MarkAsReadAsync(id);
            if(!result) return NotFound();
            return Ok(result);
        }

        //mark as unread
        [HttpPut("mark-unread/{id}")]
        public async Task<IActionResult> MarkUnread(int id)
        {
            var result = await _service.MarkAsUnReadAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("my-messages")]
        public async Task<IActionResult> GetMyMessages()
        {
            string email = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _service.GetByUserAsync(email);
            return Ok(result);
        }
    }
}
