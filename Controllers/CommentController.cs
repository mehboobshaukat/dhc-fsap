using DHC_FSAP.DTOs.Blogs;
using DHC_FSAP.Services.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace DHC_FSAP.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        //Add comment
        [HttpPost("create")]
        public async Task<IActionResult> Create(CommentCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        //get by blog
        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetByBlog(int blogId)
        {
            var result = await _service.GetByBlogIdAsync(blogId);
            return Ok(result);
        }

        //get all by admin
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        //Admin approve
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id, [FromQuery] bool status)
        {
            var resutl = await _service.ApproveAsync(id, status);
            return Ok(resutl);
        }

        //Admin Delete (it's soft delete not a hard delete)
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
