using DHC_FSAP.DTOs.Portfolios;
using DHC_FSAP.Services.Portfolios;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/portfolios")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _service;

        public PortfolioController(IPortfolioService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured()
            => Ok(await _service.GetFeaturedAsync());

        [HttpGet("get-by-slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await _service.GetBySlugAsync(slug);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] PortfolioCreateDTO dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] PortfolioUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
