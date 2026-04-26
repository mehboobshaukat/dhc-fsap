using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Services.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service) 
        {
            _service = service;
        }

        //get all pin
        [HttpGet("get-all-pin")]
        public async Task<IActionResult> GetAllPinAsync()
        {
            return Ok(await _service.GetAllPinAsync());
        }

        // get all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        //get by slug
        [HttpGet("get-by-slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await _service.GetBySlugAsync(slug);
            if (result == null) return NotFound();
            return Ok(result);
        }

        //create
        [HttpPost("create")]
        public async Task<IActionResult> Create(BlogCreateDTO dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        //update
        [HttpPut("update")]
        public async Task<IActionResult> Update(BlogUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (!result) return NotFound();
            return Ok(result);
        }

        //delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
