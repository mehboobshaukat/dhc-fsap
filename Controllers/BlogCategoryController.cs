using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Services.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/blog-category")]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _service;

        public BlogCategoryController(IBlogCategoryService service)
        {
            _service = service;
        }

        //get all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        //get by id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        //create category
        [HttpPost("create-category")]
        public async Task<IActionResult> Create(BlogCategoryCreateDTO dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        //update category
        [HttpPut("upate-category")]
        public async Task<IActionResult> Update(BlogCategoryUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if(!result) return NotFound();
            return Ok(result);
        }

        //delete category
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if(!result) return NotFound();
            return Ok(result);
        }
    }
}
