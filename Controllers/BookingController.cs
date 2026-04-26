using DHC_FSAP.DTOs.Booking;
using DHC_FSAP.Services.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        //create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(BookingCreateDTO dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _service.CreateAsync(userId, dto);
            return Ok(result);
        }

        //get all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        //get booking for a specific user
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult>  GetByUser(int userId)
        {
            var result = await _service.GetByUserAsync(userId);
            return Ok(result);
        }

        //update or reply from admin
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, BookingUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        //delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
