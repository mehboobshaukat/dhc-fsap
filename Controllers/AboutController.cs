using DHC_FSAP.DTOs.Abouts;
using DHC_FSAP.Services.About;
using Microsoft.AspNetCore.Mvc;


namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/about")]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        //public side about get
        [HttpGet("get-about")]
        public async Task<IActionResult> Get()
        {
            var result = await _aboutService.GetAsync();
            return Ok(result);
        }

        //admin side about udpate
        [HttpPut("update-about")]
        public async Task<IActionResult> Update(AboutDTO dto)
        {
            var result = await _aboutService.UpdateAsync(dto);
            return Ok(result);
        }
    }
}
